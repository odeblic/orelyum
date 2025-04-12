public class Formatter(int width = 8, int precision = 2)
{
    readonly int width = width;
    readonly int precision = precision;

    public string Direction(Trade.Direction direction)
    {
        
        switch (direction)
        {
            case Trade.Direction.BUY:
                return "buy".PadRight(6);
            case Trade.Direction.SELL:
                return "sell".PadRight(6);
            default:
                throw new ArgumentException("Invalid trade direction");
        }
    }

    public string Symbol(string symbol)
    {
        return symbol.PadRight(6);
    }

    public string Quantity(int quantity)
    {
        return quantity.ToString().PadLeft(width);
        //return quantity.ToString($"D{width}");
    }

    public string Price(decimal price)
    {
        return price.ToString($"F{precision}").PadLeft(width);
        //return $"{price:F{precision}}".PadLeft(width);
        //return string.Format($"{{0,{width:F{precision}}}}", price);;
    }
}
