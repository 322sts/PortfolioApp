using PortfolioApp.Core.Models;
using PortfolioApp.Core.Services;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Stock apple = new Stock("AAPL", "Apple");
        Console.WriteLine(apple.Symbol + " - " + apple.Name);
        Portfolio portfolio = new Portfolio();
        IMarketDataProvider marketData = new FakeMarketDataProvider();
        portfolio.Cash = 1000m;
        portfolio.Positions.Add(new Position("AAPL", 3, 285.45m));
        portfolio.Positions.Add(new Position("MSFT", 4, 430.20m));
        portfolio.Positions.Add(new Position("SPY", 7, 800.49m));
        Console.WriteLine($"Cash :{portfolio.Cash}€");
        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"================================================== REFRESH {i} ==================================================");

            foreach (Position p in portfolio.Positions)
            {
                decimal price = marketData.GetPrice(p.Symbol);
                decimal marketValue = p.Quantity * price;
                decimal gain = marketValue - p.Quantity * p.AveragePrice;
                Console.WriteLine($"{p.Symbol,-6} | {p.Quantity,5} Stocks | Unit Price : {p.AveragePrice,8:F2} € | Value : {marketValue,10:F2} € | Gain : {gain,10:F2} €");
            }

            marketData.RefreshPrices();
        }

    }
}