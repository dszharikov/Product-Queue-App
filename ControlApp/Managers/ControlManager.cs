using ControlApp.Models;
using ControlApp.Networking;

namespace ControlApp.Managers;

public class ControlManager
{
    private readonly CommandSender _commandSender;

    public ControlManager()
    {
        _commandSender = new CommandSender("127.0.0.1", 5000); // Адрес и порт для связи с QueueVisualizer
    }

    public void SendAddProductCommand(string productType)
    {
        var command = new Command
        {
            Action = "add",
            Type = productType
        };
        _commandSender.SendCommand(command);
    }

    public void SendRemoveProductCommand()
    {
        var command = new Command
        {
            Action = "remove"
        };
        _commandSender.SendCommand(command);
    }
}
