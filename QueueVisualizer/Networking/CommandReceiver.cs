using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using QueueVisualizer.Managers;
using QueueVisualizer.Models;

namespace QueueVisualizer.Networking;

public class CommandReceiver
{
    private readonly TcpListener _listener;
    private readonly QueueManager _queueManager;
    private readonly Action _updateUI;

    public CommandReceiver(QueueManager queueManager, Action updateUI, int port = 5000)
    {
        _listener = new TcpListener(IPAddress.Loopback, port);
        _queueManager = queueManager;
        _updateUI = updateUI;
    }

    public void StartListening()
    {
        _listener.Start();
        Task.Run(async () =>
        {
            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        });
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        using (var stream = client.GetStream())
        {
            var buffer = new byte[1024];
            var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var command = JsonSerializer.Deserialize<Command>(message);
            ProcessCommand(command);

            var responseMessage = "OK";
            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }

    private void ProcessCommand(Command command)
    {
        if (command.Action == "add")
        {
            _queueManager.AddProduct(command.Type);
        }
        else if (command.Action == "remove")
        {
            _queueManager.RemoveProduct();
        }
        _updateUI?.Invoke();
    }
}
