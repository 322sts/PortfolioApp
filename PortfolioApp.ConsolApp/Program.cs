using PortfolioApp.Core.Models;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Stock apple = new Stock("AAPL", "Apple");
        Console.WriteLine(apple.Symbol + " - " + apple.Name);
        Portfolio portfolio = new Portfolio();
        portfolio.Cash = 1000m;
        portfolio.Positions.Add(new Position("AAPL", 300, 25.45m));
        portfolio.Positions.Add(new Position("MSFT", 10, 410.20m));
        portfolio.Positions.Add(new Position("S&P500", 700, 2700.49m));
        Console.WriteLine($"Cash :{portfolio.Cash}€");
        foreach (Position p in portfolio.Positions)
        {
            Console.WriteLine($"{p.Symbol,-6} | {p.Quantity,5} titres | PRU : {p.AveragePrice,8:F2} €");
        }

    }
}