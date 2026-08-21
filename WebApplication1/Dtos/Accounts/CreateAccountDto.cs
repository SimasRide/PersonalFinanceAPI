using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;
using WebApplication1.Models;
namespace WebApplication1.Dtos.Accounts;

public sealed class CreateAccountDto
{
    [Required(ErrorMessage = "The account name is Mandatory.")]
    [StringLength(
        25,
        MinimumLength = 3,
        ErrorMessage = "The name must have beetween 3 and 25 characters."
        )]
    public required string Name { get; init; }

    [EnumDataType(typeof(AccountType),
        ErrorMessage = "The account name is not Valid.")]
    public AccountType Type { get; init; }

    [Range(
        typeof(decimal),
        "0.00",
             "9999999999999999.99",
        ErrorMessage = "The init balance can't be negative.")]
    public decimal InitialBalance { get; init; }

    [Required(ErrorMessage = "The currency is mandatory")]
    [RegularExpression(
        "^[A-Z] {3}$",

        ErrorMessage = " The currency must have 3 letters, for example (EUR,USD).")]
    public string Currency { get; init; } = "EUR";
}