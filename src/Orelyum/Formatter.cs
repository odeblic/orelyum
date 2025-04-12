namespace Orelyum
{
    public class Formatter(int width = 8, int precision = 2)
    {
        readonly int width = width;
        readonly int precision = precision;

        public string Direction(Trade.Direction direction)
        {
            int width = this.width > 2 ? this.width : 2;

            return direction switch
            {
                Trade.Direction.BUY => "buy".PadRight(width - 2),
                Trade.Direction.SELL => "sell".PadRight(width - 2),
                _ => throw new ArgumentException("Invalid trade direction"),
            };
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
}
