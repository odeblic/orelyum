namespace Orelyum
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filePathTrades;
                string filePathPrices;
                var booking = new Booking();

                if (args.Length == 1)
                {
                    filePathTrades = args[0];
                    booking.LoadTrades(filePathTrades);

                    ShowTrades(booking);
                    ShowPositions(booking);
                }
                else if (args.Length == 2)
                {
                    filePathTrades = args[0];
                    filePathPrices = args[1];
                    booking.LoadTrades(filePathTrades);
                    booking.LoadPrices(filePathPrices);

                    ShowTrades(booking);
                    ShowPrices(booking);
                    ShowPositions(booking);
                    ShowMarkToMarket(booking);
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

        static void ShowTrades(Booking booking)
        {
            var terminal = new Terminal(true);

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

        static void ShowPrices(Booking booking)
        {
            var terminal = new Terminal(true);

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

        static void ShowPositions(Booking booking)
        {
            var terminal = new Terminal(true);

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

        static void ShowMarkToMarket(Booking booking)
        {
            var terminal = new Terminal(true);

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
