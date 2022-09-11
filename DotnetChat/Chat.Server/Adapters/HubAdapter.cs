using Chat.Server.Ports;
using Chat.Server.Ports.Database.Services;
using Chat.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Server.Adapters
{
    public class HubAdapter : Hub
    {
        private const string _chatRoom = "Public";

        private static readonly Dictionary<string, string> _connectedUsers = new Dictionary<string, string>();
        private static readonly IMessageInterceptor _interceptor = new MessageInterceptor();

        private IMessageService _messageService;

        public HubAdapter(IMessageService messageService)
        {
            _messageService = messageService;
        }


        public async Task SendMessage(string username, string message)
        {
            _messageService.Add(username, message, _chatRoom);
            await Clients.All.SendAsync(Messages.RECEIVE, username, message);

            if (_interceptor.Intercept(ref message))
            {
                _messageService.Add(username, message, _chatRoom);
                await Clients.All.SendAsync(Messages.RECEIVE, _interceptor.GetBotName(), message);
            }
        }

        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;
            if (!_connectedUsers.ContainsKey(currentId))
            {
                _connectedUsers.Add(currentId, username);
                await Clients.AllExcept(currentId).SendAsync(Messages.RECEIVE, username, "joined the chat");

                var historic = _messageService.GetHistoric(_chatRoom);
                foreach (var item in historic)
                {
                    await Clients.Caller.SendAsync(Messages.RECEIVE, item.Username, item.Text);
                }
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            // try to get connection
            string id = Context.ConnectionId;
            if (!_connectedUsers.TryGetValue(id, out string username))
                username = "[unknown]";

            Console.WriteLine($"Disconnected {e?.Message} {username}");
            _connectedUsers.Remove(id);
            await Clients.AllExcept(Context.ConnectionId).SendAsync(Messages.RECEIVE, username, $"left the chat");
            await base.OnDisconnectedAsync(e);
        }


    }
}
