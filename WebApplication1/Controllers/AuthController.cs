using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    // O Identity gere a criação de utilizadores e as password
    private readonly UserManager<ApplicationUser> _userManager;
private readonly SignInManager<ApplicationUser> _signInManager;

public AuthController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
{
    _userManager = userManager;
    _signInManager = signInManager;
}

    // Recebe POST /api/auth/register com os dados do formulário.
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        
        var email = request.Email.Trim();

        // Verifica se este email já está registado.
        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            return Conflict(new
            {
                message = "There is already a user with this email."
            });
        }

       var user = new ApplicationUser
{
    // Usamos o email como identificador de login no Identity.
    // Assim, o nome apresentado pode conter espaços.
    UserName = email,
    Email = email,

    // Guarda aqui o nome preenchido no formulário.
    DisplayName = request.Username.Trim()
};

        // Valida a password, gera o hash e guarda o utilizador.
        // Não guardamos a password diretamente numa propriedade.
        var result = await _userManager.CreateAsync(
            user, request.Password);

        if (!result.Succeeded)
        {
            // Devolve os erros, por exemplo uma password inválida.
            return BadRequest(new
            {
                message = "Failed to create the user.",
                errors = result.Errors
                    .Select(error => error.Description)
                    .ToArray()
            });
        }

        // Devolve apenas dados públicos, sem password ou hash.
       return StatusCode(201, new
{
    message = "Utilizador criado com sucesso.",
    userId = user.Id,
    username = user.DisplayName,
    email = user.Email
});
    }
}

// Define os campos esperados no JSON enviado pelo React.
// O [ApiController] rejeita automaticamente dados inválidos.
public class RegisterRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
