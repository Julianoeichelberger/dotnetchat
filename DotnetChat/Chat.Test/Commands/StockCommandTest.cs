using Chat.Server.Ports.Commands; 
using NUnit.Framework;
using Chat.Server.Adapters.Commands;

namespace Chat.Test.Commands
{
    [TestFixture]
    public class StockCommandTest
    {  

        [Test]
        [TestCase("/stock=", "aapl.us")] 
        public void CommandTest(string identifier, string stock)
        { 
            var cmd = new StockCommand(identifier + stock);

            var response = cmd.Execute();

            Assert.That(identifier + stock, !Is.EqualTo(response));
            Assert.That(response.ToLower().Contains(stock));
        }

        [Test]
        [TestCase("/stock=", "invalid name")]
        public void BadCommandTest(string identifier, string stock)
        {
            var cmd = new StockCommand(identifier + stock);

            var response = cmd.Execute();
             
            Assert.That(!response.ToLower().Contains(stock));
        }
    }
}
