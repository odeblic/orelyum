namespace Orelyum
{
    public class Terminal(bool color = false)
    {
        readonly Formatter formatter = new Formatter();
        readonly bool color = color;

        public void Title(string title)
        {
            if (color)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            title = "[" + title.ToUpper() + "]\n";
            Console.Write(title);
        }

        public void Direction(Trade.Direction direction)
        {
            if (color)
            {
                switch (direction)
                {
                    case Trade.Direction.BUY:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Trade.Direction.SELL:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        throw new AppError("Invalid trade direction");
                }
            }

            Console.Write(formatter.Direction(direction));
        }

        public void Symbol(string symbol)
        {
            if (color)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write(formatter.Symbol(symbol));
        }

        public void Quantity(int quantity)
        {
            if (color)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            Console.Write(formatter.Quantity(quantity));
        }

        public void Price(decimal price)
        {
            if (color)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            Console.Write(formatter.Price(price));
        }

        public void Position(int quantity)
        {
            if (color)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            Console.Write(formatter.Quantity(Math.Abs(quantity)));

            if (color)
            {
                Console.ResetColor();
            }

            if (quantity > 0)
            {
                Console.Write("  (long)");
            }
            else if (quantity < 0)
            {
                Console.Write("  (short)");
            }
        }

        public void NewLine()
        {
            if (color)
            {
                Console.ResetColor();
            }

            Console.Write("\n");
        }
    }
}
