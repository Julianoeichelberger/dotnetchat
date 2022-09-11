namespace Chat.Server.Ports
{
    public interface IMessageInterceptor
    {
        public string GetBotName();

        public bool Intercept(ref string message);
    }
}
