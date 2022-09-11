using Chat.Server.Adapters.Database;
using Microsoft.EntityFrameworkCore;

namespace Chat.Database.Test
{
    public static class Context
    {
        public static MessageContext GetMessageInstance()
        {
            var options = new DbContextOptionsBuilder<MessageContext>();

            options.UseInMemoryDatabase("Messages");

            return new MessageContext(options.Options);
        }
    }
}
