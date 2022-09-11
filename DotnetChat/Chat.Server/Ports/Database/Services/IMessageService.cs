using System.Collections.Generic;

namespace Chat.Server.Ports.Database.Services
{
    public interface IMessageService
    {
        void Add(string userName, string message, string chatRoom);

        List<Models.Message> GetHistoric(string chatRoom, int pageCount = 50);
    }
}
