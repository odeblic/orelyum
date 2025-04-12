
namespace Orelyum.Tests
{
    [TestClass]
    public class BookingTest
    {
        Booking booking;

        public BookingTest()
        {
            booking = new Booking();
            string filePathTrades = Path.Combine(Directory.GetCurrentDirectory(), "Data", "trades.txt");
            string filePathPrices = Path.Combine(Directory.GetCurrentDirectory(), "Data", "prices.txt");
            booking.LoadTrades(filePathTrades);
            booking.LoadPrices(filePathPrices);
        }

        [TestMethod]
        public void LoadTrades()
        {
            var trades = booking.tradeList;

            Assert.AreEqual(8, booking.tradeList.Count(), "Wrong trade count");

            int index = 0;
            Trade trade;

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.BUY, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("A", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(100, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(5.50m, trade.Price, "Wrong trade price");

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.SELL, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("A", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(30, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(6.00m, trade.Price, "Wrong trade price");

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.SELL, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("A", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(10, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(6.10m, trade.Price, "Wrong trade price");

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.SELL, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("A", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(10, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(6.20m, trade.Price, "Wrong trade price");

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.BUY, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("B", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(80, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(10.00m, trade.Price, "Wrong trade price");

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.SELL, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("C", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(240, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(1.01m, trade.Price, "Wrong trade price");

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.BUY, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("D", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(20, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(3.10m, trade.Price, "Wrong trade price");

            trade = trades[index++];
            Assert.AreEqual(Trade.Direction.SELL, trade.Dir, "Wrong trade direction");
            Assert.AreEqual("D", trade.Symbol, "Wrong trade symbol");
            Assert.AreEqual(20, trade.Quantity, "Wrong trade quantity");
            Assert.AreEqual(3.30m, trade.Price, "Wrong trade price");
        }

        [TestMethod]
        public void LoadPrices()
        {
            var prices = booking.marketPrices;

            Assert.AreEqual(3, prices.Count(), "Wrong price count");

            bool present;
            decimal price;

            present = prices.TryGetValue("A", out price);
            Assert.IsTrue(present, "Missing price for A");
            Assert.AreEqual(6.05m, price, "Wrong price for A");

            present = prices.TryGetValue("B", out price);
            Assert.IsTrue(present, "Missing price for B");
            Assert.AreEqual(10.00m, price, "Wrong price for B");

            present = prices.TryGetValue("C", out price);
            Assert.IsTrue(present, "Missing price for C");
            Assert.AreEqual(1.01m, price, "Wrong price for C");
        }

        [TestMethod]
        public void CalculatePositions()
        {
            var positions = booking.CalculatePositions();

            Assert.AreEqual(4, positions.Count(), "Wrong position count");

            bool present;
            int quantity;

            present = positions.TryGetValue("A", out quantity);
            Assert.IsTrue(present, "Missing position for A");
            Assert.AreEqual(50, quantity, "Wrong quantity for A");

            present = positions.TryGetValue("B", out quantity);
            Assert.IsTrue(present, "Missing position for B");
            Assert.AreEqual(80, quantity, "Wrong quantity for B");

            present = positions.TryGetValue("C", out quantity);
            Assert.IsTrue(present, "Missing position for C");
            Assert.AreEqual(-240, quantity, "Wrong quantity for C");

            present = positions.TryGetValue("D", out quantity);
            Assert.IsTrue(present, "Missing position for D");
            Assert.AreEqual(0, quantity, "Wrong quantity for D");
        }

        [TestMethod]
        public void CalculateMarkToMarket()
        {
            var m2m = booking.CalculateMarkToMarket();

            Assert.AreEqual(3, m2m.Count(), "Wrong mark count");

            bool present;
            decimal mark;

            present = m2m.TryGetValue("A", out mark);
            Assert.IsTrue(present, "Missing mark for A");
            Assert.AreEqual(50m * 6.05m, mark, "Wrong mark for A");

            present = m2m.TryGetValue("B", out mark);
            Assert.IsTrue(present, "Missing mark for B");
            Assert.AreEqual(80m * 10.00m, mark, "Wrong mark for B");

            present = m2m.TryGetValue("C", out mark);
            Assert.IsTrue(present, "Missing mark for C");
            Assert.AreEqual(-240 * 1.01m, mark, "Wrong mark for C");
        }
    }
}
