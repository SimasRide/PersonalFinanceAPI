using System;
using System.Collections.Generic;
using System.Text;
// Usings padrão para este modelo




namespace WebApplication1.Models
{
    public class Account
    {       
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 

        public AccountType Type { get; set; }
        public decimal InitialBalance { get; set; }

        public string Currency { get; set; } = "EUR";
        public ICollection<Transaction> Transactions { get; set; }
        = new List<Transaction>();

    }
}
