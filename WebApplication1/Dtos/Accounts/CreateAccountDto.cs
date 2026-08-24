using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dtos.Accounts;

public sealed class CreateAccountDto
{
    // Nome da conta enviado pelo utilizador.
    [Required(ErrorMessage = "The account name is mandatory.")]
    [StringLength(
        25,
        MinimumLength = 3,
        ErrorMessage = "The name must have between 3 and 25 characters.")]
    public required string Name { get; init; }

    // Tipo da conta, limitado aos valores definidos em AccountType.
    [EnumDataType(
        typeof(AccountType),
        ErrorMessage = "The account type is not valid.")]
    public AccountType Type { get; init; }

    // Saldo existente no momento em que a conta é criada.
    [Range(
        typeof(decimal),
        "0.00",
        "9999999999999999.99",
        ErrorMessage = "The initial balance cannot be negative.")]
    public decimal InitialBalance { get; init; }

    // Código da moeda com exatamente três letras maiúsculas.
    [Required(ErrorMessage = "The currency is mandatory.")]
    [RegularExpression(
        "^[A-Z]{3}$",
        ErrorMessage = "The currency must have 3 letters, for example EUR.")]
    public string Currency { get; init; } = "EUR";
}