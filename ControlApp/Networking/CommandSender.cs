using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ControlApp.Models;

namespace ControlApp.Networking;

public class CommandSender
{
    private readonly string _ipAddress;
    private readonly int _port;

    public CommandSender(string ipAddress, int port)
    {
        _ipAddress = ipAddress;
        _port = port;
    }

    public void SendCommand(Command command)
    {
        using (var client = new TcpClient())
        {
            client.Connect(_ipAddress, _port);
            var stream = client.GetStream();

            var message = JsonSerializer.Serialize(command);
            var data = Encoding.UTF8.GetBytes(message);

            stream.Write(data, 0, data.Length);
        }
    }
}
