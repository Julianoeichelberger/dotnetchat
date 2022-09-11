using Chat.Server.Ports.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Server.Adapters.Database
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options) : base(options) { }

        public DbSet<Message> Message { get; set; }
    }
}
