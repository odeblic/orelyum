using System.Text.RegularExpressions;

namespace Orelyum
{
    public class Booking
    {
        public readonly List<Trade> tradeList;
        public readonly Dictionary<string, decimal> marketPrices;

        public Booking()
        {
            tradeList = [];
            marketPrices = [];
        }

        public void LoadTrades(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File not found");
            }

            foreach (string line in File.ReadLines(filePath))
            {
                Trade trade = new Trade(line);
                tradeList.Add(trade);
            }
        }

        public void LoadPrices(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File not found");
            }

            foreach (string line in File.ReadLines(filePath))
            {
                string[] fields = Regex.Split(line, @"\s+");

                if (fields.Length == 2)
                {
                    string symbol = fields[0];
                    decimal price = decimal.Parse(fields[1]);
                    marketPrices.Add(symbol, price);
                }
                else
                {
                    throw new ArgumentException("Invalid price field count");
                }
            }
        }

        public Dictionary<string, int> CalculatePositions()
        {
            var positions = new Dictionary<string, int>();

            foreach (var trade in tradeList)
            {
                int tradedQuantity = trade.Quantity;

                if (trade.Dir == Trade.Direction.SELL)
                {
                    tradedQuantity = -tradedQuantity;
                }

                if (positions.TryGetValue(trade.Symbol, out int cumulatedQuantity))
                {
                    positions[trade.Symbol] = cumulatedQuantity + tradedQuantity;
                }
                else
                {
                    positions.Add(trade.Symbol, tradedQuantity);
                }
            }

            return positions;
        }

        public Dictionary<string, decimal> CalculateMarkToMarket()
        {
            var positions = CalculatePositions();
            var m2m = new Dictionary<string, decimal>();

            foreach (var position in positions)
            {
                string symbol = position.Key;
                int quantity = position.Value;

                if (quantity != 0)
                {
                    if (!marketPrices.TryGetValue(symbol, out decimal price))
                    {
                        throw new ArgumentException("Price not found for given symbol");
                    }

                    m2m.Add(symbol, price * quantity);
                }
            }

            return m2m;
        }
    }
}
