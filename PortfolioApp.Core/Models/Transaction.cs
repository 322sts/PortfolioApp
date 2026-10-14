using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioApp.Core.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Fees { get; set; }

        public Transaction(DateTime date, TransactionType type, string symbol, int quantity, decimal unitPrice, decimal fees)
        {
            Date = date;
            Type = type;
            Symbol = symbol;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Fees = fees;
        }

    }
}
