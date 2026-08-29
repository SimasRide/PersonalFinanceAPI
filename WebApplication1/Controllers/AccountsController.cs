// Permite criar Controllers, rotas HTTP e respostas como Ok().
// Também disponibiliza atributos como [ApiController], [Route] e [HttpGet].
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
// Disponibiliza métodos assíncronos do Entity Framework Core,
// como ToListAsync(), e configurações como AsNoTracking().
using Microsoft.EntityFrameworkCore;

// Importa o namespace onde está o AppDBContext,
// responsável pela comunicação com a base de dados.
using WebApplication1.Data;

// Importa os DTOs relacionados com contas.
// Neste caso, precisamos do AccountResponseDto.
using WebApplication1.Dtos.Accounts;


// Define o namespace ao qual este controller pertence.
//
// O namespace ajuda a organizar e identificar as classes do projeto.
namespace WebApplication1.Controllers;


//  APICONTROLLER Informa ao ASP.NET Core que esta classe é um Controller de API :
// - validação dos DTOs;
// - respostas HTTP de erro;
// - leitura de JSON enviado pelo cliente.
[ApiController]

// Define a rota base deste controller.
//
// [controller] é substituído pelo nome da classe sem "Controller".
//
// AccountsController transforma-se em:
//
// /api/accounts
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    /*
     Resumo das alterações neste controller:
     - Adicionados endpoints PUT /api/accounts/{id} e DELETE /api/accounts/{id}
       para completar o CRUD de contas.
     - O PUT procura a conta pelo id, valida os dados (ModelState), aplica
       as alterações do DTO e grava no banco (SaveChangesAsync).
     - O DELETE remove a conta encontrada e grava as alterações.
     - Os métodos devolvem códigos HTTP apropriados: 400, 404, 204, 500.

     Observações relacionadas ao projeto:
     - Os enums (AccountType) estão configurados para serem serializados
       como strings no JSON (configuração em Program.cs).
     - A propriedade Transactions em Account é pública para que o EF
       possa mapear a relação 1:N corretamente.
     - Testa estes endpoints com Postman/Insomnia ou via browser (GET).
    */
    
    // readonly significa que a referência só pode ser atribuída
    // durante a declaração ou dentro do construtor.
    
    // O prefixo _db é uma convenção para campos privados.
    private readonly AppDBContext _dbContext;


    // Construtor do AccountsController.
    //
    // O ASP.NET Core cria automaticamente o controller quando recebe
    // um pedido e fornece uma instância do AppDBContext.
    //
    // Isto chama-se Dependency Injection.
    public AccountsController(AppDBContext dbContext)
    {
        // Guarda o DbContext recebido para podermos utilizá-lo
        // nos endpoints deste controller.
        _dbContext = dbContext;
    }


    // GET /api/accounts
    [HttpGet]

    // async permite que o método aguarde operações da base de dados
    // sem bloquear a aplicação.

    // Task representa uma operação assíncrona.

    // List<AccountResponseDto> representa os dados esperados
    // quando a operação termina com sucesso.
    public async Task<ActionResult<List<AccountResponseDto>>> GetAll()

    {
        // _dbContext.Accounts representa a tabela Accounts
        // existente no PostgreSQL.
        var accounts = await _dbContext.Accounts

            // Informa o Entity Framework de que os resultados são
            // apenas para consulta e não serão alterados
            // Isto reduz o trabalho de memória e melhora o desempenho
            // dos pedidos que apenas leem dados.
            .AsNoTracking()

            // Select converte cada Account da base de dados
            // num AccountResponseDto.

            // Desta forma, a API não expõe diretamente a entidade
            // utilizada pelo Entity Framework.
            .Select(account => new AccountResponseDto(
                account.Id,
                account.Name,
                account.Type,
                account.InitialBalance,
                account.Currency))

            // Executa a consulta no PostgreSQL e transforma
            // os resultados numa lista.
            
            // O sufixo Async indica que a operação é assíncrona.
            .ToListAsync();
  
        // O ASP.NET Core transforma automaticamente a lista
        // de DTOs em JSON.
        return Ok(accounts);
    }

    // POST /api/accounts
    [HttpPost]
    //	A Assinatura: é assíncrono e retorna ActionResult<AccountResponseDto>
    //	— pode devolver respostas HTTP com um DTO no corpo
    public async Task<ActionResult<AccountResponseDto>> Create( //	Parâmetro CreateAccountDto request: o ASP.NET faz model binding
                                                                //dos dados do pedido para este DTO (normalmente JSON).
        CreateAccountDto request)
    {
        var account = new Account
        {
            Name = request.Name.Trim(),
            Type = request.Type,
            InitialBalance = request.InitialBalance,
            Currency = request.Currency
        };

        _dbContext.Accounts.Add(account);
        Console.WriteLine($"A tentar guardar a conta: {account.Name}");

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return StatusCode(500, new
            {
                error = exception.Message,
                innerError = exception.InnerException?.Message
            });
        }

        var response = new AccountResponseDto(
            account.Id,
            account.Name,
            account.Type,
            account.InitialBalance,
            account.Currency);

        return Created($"/api/accounts/{account.Id}", response);
    }


    // PUT /api/accounts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateAccountDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var account = await _dbContext.Accounts.FindAsync(id);
        if (account is null)
            return NotFound();

        // Aplica alterações recebidas no DTO
        account.Name = request.Name.Trim();
        account.Type = request.Type;
        account.InitialBalance = request.InitialBalance;
        account.Currency = request.Currency;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }

        // Sucesso sem corpo
        return NoContent();
    }

    // DELETE /api/accounts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _dbContext.Accounts.FindAsync(id);
        if (account is null)
            return NotFound();

        _dbContext.Accounts.Remove(account);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }

        return NoContent();
    }


}
