class Orelyum
{
    static void Main(string[] args)
    {
        if (args.Length == 1)
        {
            ProcessTrades(args[0]);
        }
        else
        {
            DisplayHelp();
        }
    }

    static void ProcessTrades(string filePath)
    {
        var tradeList = Booking.LoadTrades(filePath);
        var terminal = new Terminal(true);

        terminal.Title("trades");
        terminal.NewLine();

        foreach (var trade in tradeList)
        {
            terminal.Direction(trade.Dir);
            terminal.Symbol(trade.Symbol);
            terminal.Quantity(trade.Quantity);
            terminal.Price(trade.Price);
            terminal.NewLine();
        }

        Dictionary<string, int> positions = Booking.ComputePositions(tradeList);

        terminal.NewLine();
        terminal.Title("positions");
        terminal.NewLine();

        foreach (KeyValuePair<string, int> kvp in positions)
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
    }

    static void DisplayHelp()
    {
        Console.WriteLine("Description:");
        Console.WriteLine("    This program loads a batch of booked trades and calculate the positions.");
        Console.WriteLine("");
        Console.WriteLine("Usage:");
        Console.WriteLine("    $ orelyum [FILE]");
    }
}
