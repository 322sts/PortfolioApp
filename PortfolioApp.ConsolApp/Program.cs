using PortfolioApp.Core.Models;
using PortfolioApp.Core.Services;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Portfolio portfolio = new Portfolio();
        portfolio.Cash = 10000m;
        IMarketDataProvider marketData = new FakeMarketDataProvider();
        PortfolioService service = new PortfolioService(portfolio, marketData);
        service.Buy("AAPL", 5);
        marketData.RefreshPrices();
        service.Buy("AAPL", 5, 2m);
        service.Buy("MSFT", 3);
        
        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"================================================== REFRESH {i} ==================================================");

            foreach (Position p in portfolio.Positions)
            {
                decimal price = marketData.GetPrice(p.Symbol);
                decimal marketValue = p.Quantity * price;
                decimal gain = marketValue - p.Quantity * p.AveragePrice;
                Console.WriteLine($"{p.Symbol,-6} | {p.Quantity,5} Stocks | Average Price : {p.AveragePrice,8:F2} € | Value : {marketValue,10:F2} € | Gain : {gain,10:F2} €");
                
            }
            Console.WriteLine($"Cash :{portfolio.Cash:F2}€");
            marketData.RefreshPrices();
        }

    }
}