namespace Chat.Server.Ports
{
    public interface IBotCommandCheck<I> where I : IBotCommand
    {
        string Identifier { get; }

        bool Check(ref string message);

        I Command(string message);
    }
}
