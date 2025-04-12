public class Formatter(int width = 8, int precision = 2)
{
    readonly int width = width;
    readonly int precision = precision;

    public string Direction(Trade.Direction direction)
    {
        int width = this.width > 2 ? this.width : 2;

        switch (direction)
        {
            case Trade.Direction.BUY:
                return "buy".PadRight(width - 2);
            case Trade.Direction.SELL:
                return "sell".PadRight(width - 2);
            default:
                throw new ArgumentException("Invalid trade direction");
        }
    }

    public string Symbol(string symbol)
    {
        int width = this.width > 2 ? this.width : 2;
        return symbol.PadRight(width - 2);
    }

    public string Quantity(int quantity)
    {
        return quantity.ToString().PadLeft(width);
    }

    public string Price(decimal price)
    {
        return price.ToString($"F{precision}").PadLeft(width + 2);
    }
}
