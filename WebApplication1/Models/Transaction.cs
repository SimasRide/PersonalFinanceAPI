using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Models
{
    //Criei a classe pública designada de Transaction de para definir todos os dados relativos a esta operação.
    public class Transaction 
    {
  
        public int Id { get; set; }

        //descrição da operação financeira
        public string Description { get; set; } = string.Empty;

        // Valor da transação, esta em decimal 

        public decimal Amount { get; set; }

        
        public DateTime Date { get; set; }


        // Tipo da transação, por enquanto fica texto (lucro/despesas etc) logo vê-se a possibilidade de usar Enum
        public string Type { get; set; } = string.Empty;                                                           

        //FK associado
        public int AccountId { get; set; }

        // Navigation Property -> Transaction -> Property : não esquecer do uso da entity framework
        // 
        public Account BankAccount { get; set; } = null!;
    }
}