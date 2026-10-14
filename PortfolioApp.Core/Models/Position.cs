using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioApp.Core.Models
{
    public class Position
    {
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }

        public Position(string symbol, int quantity, decimal averagePrice)
        {
            Symbol = symbol;
            Quantity = quantity;
            AveragePrice = averagePrice;
        }
    }
}
