using Chat.Shared;

ChatClient? client;
string userName = string.Empty;
string url = "http://127.0.0.1:6840";
string newMessage = string.Empty;

void MessageReceived(object sender, MessageReceivedEventArgs e)
{
    if (e.Username != userName)
    {
        Console.WriteLine(e.Username + ": " + e.Message);
        Console.WriteLine();
    }
}

async Task SendAsync()
{
    if (!string.IsNullOrEmpty(newMessage))
    {
        await client.SendAsync(newMessage);
        newMessage = string.Empty;
    }
}

while (string.IsNullOrEmpty(userName))
{
    Console.WriteLine("Enter a name: ");
    userName = Console.ReadLine();
}
Console.WriteLine();
Console.WriteLine("Let's chat...");
Console.WriteLine();

client = new ChatClient(userName, url);
client.MessageReceived += MessageReceived;
await client.StartAsync();

while (true)
{
    ConsoleKeyInfo info = Console.ReadKey(true);

    while (info.Key != ConsoleKey.Enter)
    {
        if (string.IsNullOrEmpty(newMessage))
        {
            Console.Write(userName + ": ");
        }
        newMessage += info.KeyChar;
        Console.Write(info.KeyChar);
        info = Console.ReadKey(true);
    }
    Console.WriteLine();
    Console.WriteLine();
    await SendAsync();
}

