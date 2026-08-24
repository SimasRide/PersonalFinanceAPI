// Permite criar Controllers, rotas HTTP e respostas como Ok().
// Também disponibiliza atributos como [ApiController], [Route] e [HttpGet].
using Microsoft.AspNetCore.Mvc;

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

        // Devolve uma resposta HTTP 200 OK.
        
        // O ASP.NET Core transforma automaticamente a lista
        // de DTOs em JSON.
        return Ok(accounts);
    }
}