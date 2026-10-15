using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioApp.Core.Services
{
    public interface IMarketDataProvider
    {
        decimal GetPrice(string symbol);
        void RefreshPrices();
    }
}
