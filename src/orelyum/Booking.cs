using System.Text.RegularExpressions;

public class Booking
{
    static public List<Trade> LoadTrades(string filePath)
    {
        var tradeList = new List<Trade>();

        if (!File.Exists(filePath))
        {
            throw new ArgumentException("File not found");
        }

        foreach (string line in File.ReadLines(filePath))
        {
            Trade trade = new Trade(line);
            tradeList.Add(trade);
        }

        return tradeList;
    }

    static public Dictionary<string, decimal> LoadPrices(string filePath)
    {
        var prices = new Dictionary<string, decimal>();

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
                prices.Add(symbol, price);
            }
            else
            {
                throw new ArgumentException("Invalid price field count");
            }
        }

        return prices;
    }

    static public Dictionary<string, int> ComputePositions(List<Trade> tradeList)
    {
        var positions = new Dictionary<string, int>();

        foreach (var trade in tradeList)
        {
            int newQuantity = 0;

            if (trade.Dir == Trade.Direction.BUY)
            {
                newQuantity = trade.Quantity;
            }
            else
            {
                newQuantity = -trade.Quantity;
            }

            if (positions.ContainsKey(trade.Symbol))
            {
                int currentQuantity = positions[trade.Symbol];
                positions[trade.Symbol] = currentQuantity + newQuantity;
            }
            else
            {
                positions.Add(trade.Symbol, newQuantity);
            }
        }

        return positions;
    }

    static public Dictionary<string, decimal> ComputePnL(List<Trade> tradeList, Dictionary<string, int> prices)
    {
        var pnl = new Dictionary<string, decimal>();

        foreach (var trade in tradeList)
        {
            if (!pnl.ContainsKey(trade.Symbol))
            {
                pnl.Add(trade.Symbol, 0);
            }

            if (trade.Dir == Trade.Direction.BUY)
            {
                pnl[trade.Symbol] += trade.Quantity * trade.Price;
            }
            else
            {
                pnl[trade.Symbol] -= trade.Quantity * trade.Price;
            }
        }

        return pnl;
    }
}
