using System.Net.Sockets;
using System.Text.Json;
using TCPserver;

Console.WriteLine("Enter one of following commands: random add subtract");
string command = Console.ReadLine();
Console.WriteLine("Enter first value");
string value1 = Console.ReadLine();
Console.WriteLine("Enter second value");
string value2 = Console.ReadLine();

Message message = new() { Command = command, Value1 = value1, Value2 = value2 };

Console.WriteLine("Starting TCP Client");

TcpClient socket = new();
socket.Connect("127.0.0.1", 14000); //connect to localhost
NetworkStream stream = socket.GetStream();
StreamReader reader = new(stream);
StreamWriter writer = new(stream);

Console.WriteLine("Connected to the TCP server");

writer.WriteLine(JsonSerializer.Serialize(message));
writer.Flush();
Console.WriteLine(reader.ReadLine());

socket.Close();
Console.WriteLine("TCP client closed");