using Chat.Shared;

ChatClient? client;
string userName = string.Empty;
string url = "http://127.0.0.1:6841";
string newMessage = string.Empty;

void MessageReceived(object sender, MessageReceivedEventArgs e)
{ 
    Console.WriteLine(e.Username + ": " + e.Message);
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

client = new ChatClient(userName, url);
client.MessageReceived += MessageReceived;
await client.StartAsync();

while (true)
{
    newMessage = Console.ReadLine();
    if (!string.IsNullOrEmpty(newMessage))
    {
        Console.WriteLine(userName + ": " + newMessage);
        await SendAsync();
    }
}
