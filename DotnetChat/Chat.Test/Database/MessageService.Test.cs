using Chat.Server.Adapters.Database;
using Chat.Server.Adapters.Database.Services;
using Chat.Server.Ports.Database.Services;
using NUnit.Framework;

namespace Chat.Database.Test
{
    [TestFixture]
    public class MessageServiceTest
    {
        private MessageContext _context;

        [SetUp]
        public void Setup()
        {
            _context = Context.GetMessageInstance();
        }

        [Test]
        [TestCase("User1", "Message", "Public")]
        [TestCase("User2", "Message text", "Public")]
        public void AddMessageTest(string userName, string message, string chatRoom)
        {
            IMessageService service = new MessageService(_context);

            service.Add(userName, message, chatRoom);

            Assert.That(service.GetHistoric(chatRoom).Count > 0);
        }

        [Test]
        [TestCase(0)]
        [TestCase(25)]
        public void GetHistoricTest(int pageCount)
        {
            IMessageService service = new MessageService(_context);

            for (int i = 0; i < pageCount; i++)
            {
                service.Add("User", "Message " + i.ToString(), "Public");
            }

            Assert.That(service.GetHistoric("Public", pageCount).Count, Is.EqualTo(pageCount));
        }

    }
}