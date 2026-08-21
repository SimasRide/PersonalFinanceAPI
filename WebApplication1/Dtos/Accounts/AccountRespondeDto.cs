using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Dtos.Accounts
{
    public sealed record AccountRespondeDto(
        int Id,
    string Name,
   AccountType Type,
   decimal InitialBalance,
        string Currency
        );
}
