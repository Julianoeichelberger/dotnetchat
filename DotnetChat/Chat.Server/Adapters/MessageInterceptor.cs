using Chat.Server.Adapters.CommandCheck;
using Chat.Server.Ports;
using Chat.Server.Ports.Commands;

namespace Chat.Server.Adapters
{
    public class MessageInterceptor : IMessageInterceptor
    {
        private const string _botName = "Bot";

        public string GetBotName() => _botName;

        public bool Intercept(ref string message)
        {
            IBotCommandCheck<IStockCommand> cmd = new StockCommandCheck();

            if (!cmd.Check(ref message))
            {
                return false;
            }

            message = cmd.Command(message).Execute();
            return true;
        }
    }
}
