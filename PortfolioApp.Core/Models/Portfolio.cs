using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioApp.Core.Models
{
    public class Portfolio
    {
        public decimal Cash { get; set; }
        public decimal RealizedGains { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
