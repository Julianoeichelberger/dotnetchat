using Chat.Server.Adapters.Commands;
using Chat.Server.Ports;
using Chat.Server.Ports.Commands;

namespace Chat.Server.Adapters.CommandCheck
{
    public class StockCommandCheck : IBotCommandCheck<IStockCommand>
    {
        public string Identifier => "/stock=";

        public bool Check(ref string message)
        {
            if (message.ToLower().StartsWith(Identifier))
            {
                message = message.Substring(Identifier.Length);
                return true;
            }
            return false;
        }

        public IStockCommand Command(string message)
        {
            return new StockCommand(message);
        }
    }
}
