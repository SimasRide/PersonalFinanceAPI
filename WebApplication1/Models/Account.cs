using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Models
{
    public class Account
    {       
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Type { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public string Currency { get; set; } = "EUR";
        ICollection<Transaction> Transactions { get; set; }
        = new List<Transaction>();

    }
}
