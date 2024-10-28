namespace QueueVisualizer.Models;

public class Command
{
    public string Action { get; set; } // "add" or "remove"
    public string Type { get; set; }   // "good" or "bad"
}
