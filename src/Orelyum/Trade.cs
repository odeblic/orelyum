using System.Text.RegularExpressions;

namespace Orelyum
{
    public partial class Trade
    {
        [GeneratedRegex(@"\s+")]
        private static partial Regex SpacePattern();

        public enum Direction
        {
            BUY,
            SELL,
        }

        public Direction Dir { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Trade(string line)
        {
            string[] fields = SpacePattern().Split(line);

            if (fields.Length == 4)
            {
                Dir = ParseDirection(fields[0]);
                Symbol = fields[1];
                Quantity = int.Parse(fields[2]);
                Price = decimal.Parse(fields[3]);
            }
            else
            {
                throw new AppError("Invalid trade field count");
            }
        }

        public override string ToString()
        {
            return $"{Dir} {Symbol} {Quantity} {Price}";
        }

        private static Direction ParseDirection(string direction)
        {
            direction = direction.ToLower();

            if (direction == "buy")
            {
                return Direction.BUY;
            }
            else if (direction == "sell")
            {
                return Direction.SELL;
            }
            else
            {
                throw new AppError("Invalid trade direction");
            }
        }
    }
}
