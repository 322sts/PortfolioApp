using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioApp.Core.Services
{
    public class FakeMarketDataProvider : IMarketDataProvider
    {
        private Dictionary<string, decimal> _prices = new Dictionary<string, decimal>
        {
            {"AAPL", 300.76m},
            {"MSFT", 480.23m},
            {"SPY", 770.30m},
            {"TSLA", 233.98m}
        };

        public decimal GetPrice(string symbol)
        {
            return _prices[symbol];
        }

        private Random _random = new Random();

        public void RefreshPrices()
        {
            foreach (string symbol in new List<string>(_prices.Keys))
            {
                decimal variation = (decimal)(_random.NextDouble() * 0.04 - 0.02);
                _prices[symbol] = _prices[symbol] * (1 + variation);
            }
        }
    }
}
