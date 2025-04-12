namespace Orelyum
{
    class Program
    {
        readonly Booking booking;
        readonly Terminal terminal;

        private Program()
        {
            booking = new Booking();
            terminal = new Terminal(true);
        }

        public Program(string filePathTrades)
        : this()
        {
            booking.LoadTrades(filePathTrades);
        }

        public Program(string filePathTrades, string filePathPrices)
        : this(filePathTrades)
        {
            booking.LoadPrices(filePathPrices);
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1)
                {
                    Program program = new(args[0]);
                    program.ShowTrades();
                    program.ShowPositions();
                }
                else if (args.Length == 2)
                {
                    Program program = new(args[0], args[1]);
                    program.ShowTrades();
                    program.ShowPrices();
                    program.ShowPositions();
                    program.ShowMarkToMarket();
                }
                else
                {
                    DisplayHelp();
                }
            }
            catch (AppError e)
            {
                DisplayError(e);
            }
        }

        void ShowTrades()
        {
            terminal.Title("trades");
            terminal.NewLine();

            foreach (var trade in booking.tradeList)
            {
                terminal.Direction(trade.Dir);
                terminal.Symbol(trade.Symbol);
                terminal.Quantity(trade.Quantity);
                terminal.Price(trade.Price);
                terminal.NewLine();
            }

            terminal.NewLine();
        }

        void ShowPrices()
        {
            terminal.Title("market prices");
            terminal.NewLine();

            foreach (var kvp in booking.marketPrices)
            {
                var symbol = kvp.Key;
                var price = kvp.Value;

                terminal.Symbol(symbol);
                terminal.Price(price);
                terminal.NewLine();
            }

            terminal.NewLine();
        }

        void ShowPositions()
        {
            Dictionary<string, int> positions = booking.CalculatePositions();

            terminal.Title("positions");
            terminal.NewLine();

            foreach (var kvp in positions)
            {
                var symbol = kvp.Key;
                var quantity = kvp.Value;

                if (quantity != 0)
                {
                    terminal.Symbol(symbol);
                    terminal.Position(quantity);
                    terminal.NewLine();
                }
            }

            terminal.NewLine();
        }

        void ShowMarkToMarket()
        {
            Dictionary<string, decimal> m2m = booking.CalculateMarkToMarket();

            terminal.Title("mark to market");
            terminal.NewLine();

            foreach (var kvp in m2m)
            {
                var symbol = kvp.Key;
                var mark = kvp.Value;

                terminal.Symbol(symbol);
                terminal.Price(mark);
                terminal.NewLine();
            }

            terminal.NewLine();
        }

        static void DisplayHelp()
        {
            Console.WriteLine("Description:");
            Console.WriteLine("    This program loads a batch of booked trades and calculate the positions.");
            Console.WriteLine("");
            Console.WriteLine("Usage:");
            Console.WriteLine("    $ orelyum [TRADE_FILE]");
            Console.WriteLine("    $ orelyum [TRADE_FILE] [PRICE_FILE]");
        }

        static void DisplayError(AppError e)
        {
            Console.WriteLine("Error:");
            Console.WriteLine($"    {e.Message}");

            if (e.InnerException != null)
            {
                Console.WriteLine($"    {e.InnerException.Message}");
            }
        }
    }
}
