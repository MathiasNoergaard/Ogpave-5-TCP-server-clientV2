using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using TCPserver;

string CommandLogic(Message message)
{
    message.Command = message.Command.ToLower().Trim();
    int value1;
    int value2;

    if (Int32.TryParse(message.Value1, out value1) == false)
    {
        return "First value is invalid";
    }

    if (Int32.TryParse(message.Value2, out value2) == false)
    {
        return "Second value is invalid";
    }

    switch (message.Command)
    {
        case "random":
            Random randomGenerator = new();
            return $"{randomGenerator.Next(value1, value2)}";
        case "add":
            return $"{value1 + value2}";
        case "subtract":
            return $"{value1 - value2}";
        default:
            return "Command not recognised";
    }
}

void WriteToClient(StreamWriter writer, string message)
{
    writer.WriteLine(message);
    writer.Flush();
}

void HandleClient(TcpClient client)
{
    NetworkStream stream = client.GetStream();
    StreamReader reader = new(stream);
    StreamWriter writer = new(stream);
    Console.WriteLine("Client connected");

    string receivedMessage = reader.ReadLine();
    Message message = JsonSerializer.Deserialize<Message>(receivedMessage);

    writer.WriteLine(CommandLogic(message));
    writer.Flush();

    client.Close();
    Console.WriteLine("Closed connection with client");
}

Console.WriteLine("Starting TCP server");
TcpListener listener = new(IPAddress.Any, 14000);
listener.Start();
Console.WriteLine("TCP server started");

while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    Thread thread = new(() => HandleClient(socket));
    thread.Start();
}

listener.Stop();
Console.WriteLine("Server stopped");