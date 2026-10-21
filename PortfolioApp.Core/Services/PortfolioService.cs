using PortfolioApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PortfolioApp.Core.Services
{
    public class PortfolioService
    {
        private readonly Portfolio _portfolio;
        private readonly IMarketDataProvider _marketData;

        public PortfolioService(Portfolio portfolio, IMarketDataProvider marketData)
        {
            _portfolio = portfolio;
            _marketData = marketData;
        }

        public void Buy(string symbol, int quantity, decimal fees = 0m)
        {
            // 1. Vérifier la quantité
            if (quantity <= 0)
            {
                throw new ArgumentException("You can't buy 0 or fewer stocks.");
            }

            // 2. Récupérer le prix actuel
            decimal price = _marketData.GetPrice(symbol);

            // 3. Calculer le coût total
            decimal totalCost = quantity * price + fees;

            // 4. Vérifier que l'argent suffit
            if (totalCost > _portfolio.Cash)
            {
                throw new InvalidOperationException("Not enough cash.");
            }

            // 5. Chercher si on a déjà une position sur ce symbole
            Position? existing = null;

            foreach (Position p in _portfolio.Positions)
            {
                if (p.Symbol == symbol)
                {
                    existing = p;
                    break;
                }
            }

            // 6a. Si non → ajouter une nouvelle Position
            if (existing == null)
            {
                Position newPosition = new Position(symbol, quantity, totalCost / quantity);
                _portfolio.Positions.Add(newPosition);
            }

            // 6b. Si oui → recalculer le PRU puis augmenter la quantité
            else
            {
                decimal totalInvest = existing.Quantity * existing.AveragePrice + totalCost;
                int newQuantity = quantity + existing.Quantity;
                string actualSymbol = existing.Symbol;

                existing.AveragePrice = totalInvest / newQuantity;
                existing.Quantity = newQuantity;
            }
            
            // 7. Retirer le coût total du Cash
            _portfolio.Cash = _portfolio.Cash - totalCost;

            // 8. Ajouter une Transaction dans l'historique
            _portfolio.Transactions.Add(new Transaction(DateTime.Now, TransactionType.Buy, symbol, quantity, price, fees));
            
            
            
        }

        public void Sell(string symbol, int quantity, decimal fees = 0m)
        {
            // 1. Verfify quantity
            if (quantity <= 0)
            {
                throw new ArgumentException("You can't sell 0 or negative stocks.");
            }

            // 2. Look if have position
            Position? existing = null;
            foreach (Position position in _portfolio.Positions)
            {
                if (position.Symbol == symbol)
                {
                    existing = position;
                    break;
                }
            }

            // VERIFY IF USER WONT SELL STOCKS THAT HE DOESN'T OWN
            if (existing == null)
            {
                throw new InvalidOperationException("You can't sell stocks you don't own.");
            }

            // VERIFY IF USER WONT SELL MORE THAN HE OWN
            if (quantity > existing.Quantity)
            {
                throw new InvalidOperationException("You can't sell more stocks than you own.");
            }

            // 3. Find out the price
            decimal price = _marketData.GetPrice(symbol);

            // 4. Calculate cashout
            decimal cashout = quantity * price - fees;

            // 5. Calculate the capital gains
            decimal realizedGain = (price - existing.AveragePrice) * quantity - fees;
            _portfolio.RealizedGains += realizedGain;

            int newQuantity = existing.Quantity - quantity;
            existing.Quantity = newQuantity;

            // 7. Remove the position
            if (existing.Quantity == 0)         
            {
                _portfolio.Positions.Remove(existing);   
            }

            // 8. Add cash to portfolio
            _portfolio.Cash = cashout + _portfolio.Cash;

            // 9. Save transaction to transactions history
            _portfolio.Transactions.Add(new Transaction(DateTime.Now, TransactionType.Sell, symbol, quantity, price, fees));
        }

        public decimal GetTotalValue()
        {
            // 1. Put cash in total value
            decimal total = _portfolio.Cash;

            // 2. For each add actual value
            foreach (Position position in _portfolio.Positions)
            {
                decimal price = _marketData.GetPrice(position.Symbol);
                total += position.Quantity * price;
            }

            return total;
        }

        public decimal GetUnrealizedGains()
        {
            // 1. Start unrealizedGains at 0 
            decimal unrealizedGains = 0m;

            // 2. For each add position's gains to the unrealizedGains
            foreach (Position position in _portfolio.Positions)
            {
                decimal price = _marketData.GetPrice(position.Symbol);
                decimal positionGain = (position.Quantity * price) - (position.Quantity * position.AveragePrice);
                unrealizedGains += positionGain;
            }

            return unrealizedGains;
        }



        public Dictionary<string, decimal> GetAllocation()
        {
            // 1. Create a void annuary
            Dictionary<string, decimal> allocation = new Dictionary<string, decimal>();

            // 2. Provide the total Value
            decimal total = GetTotalValue();

            // 3. If total is 0 return void annuary
            if (total == 0m)
            {
                return allocation;
            }

            Position? existing = null;
            // 4. For each : price, value, percentage, add in the annuary
            {

            }

            // 5. Pourcentage du cash, ajouté avec la clé "CASH"
            //    → à toi

            // 6. Renvoyer l'annuaire
            return allocation;
        }
    }
}
