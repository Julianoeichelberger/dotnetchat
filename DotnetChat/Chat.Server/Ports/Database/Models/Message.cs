using System;

namespace Chat.Server.Ports.Database.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Text { get; set; }

        public string Chatroom { get; set; }
    }
}
