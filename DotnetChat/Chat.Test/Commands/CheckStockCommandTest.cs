using Chat.Server.Adapters.CommandCheck;
using Chat.Server.Ports;
using Chat.Server.Ports.Commands;
using NUnit.Framework;

namespace Chat.Test.Commands
{
    [TestFixture]
    public class CheckStockCommandTest
    {
        IBotCommandCheck<IStockCommand> _command;

        [SetUp]
        public void Init()
        {
            _command = new StockCommandCheck();
        }

        [Test]
        [TestCase("/stock=aapl.us")]
        public void CheckTest(string message)
        {
            Assert.That(_command.Check(ref message));
        }

        [Test]
        [TestCase("=aapl.us")]
        [TestCase("stock=aapl.us")]
        [TestCase("/stock")]
        public void CheckFalseTest(string message)
        {
            Assert.That(!_command.Check(ref message));
        }
    }
}
