using Chat.Server.Ports.Database.Models;
using Chat.Server.Ports.Database.Services;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Server.Adapters.Database.Services
{
    public class MessageService : IMessageService
    {
        private MessageContext _context;

        public MessageService(MessageContext context)
        {
            _context = context;
        }

        public void Add(string userName, string message, string chatRoom)
        {
            var model = new Message
            {
                Username = userName,
                Text = message,
                Chatroom = chatRoom
            };

            _context.Add(model);
            _context.SaveChangesAsync();
        }

        public List<Message> GetHistoric(string chatRoom, int pageCount = 50)
        {
            var query = _context.Message
             .Where(x => x.Chatroom == chatRoom)
             .OrderBy(x => x.Timestamp)
             .Take(pageCount); 

            return query.ToList();
        }
    }
}
