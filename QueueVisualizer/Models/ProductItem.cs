using System.Windows.Media;

namespace QueueVisualizer.Models;

public class ProductItem
{
    public Color Color { get; }

    public ProductItem(Color color)
    {
        Color = color;
    }
}
