using System.Windows.Media;
using QueueVisualizer.Models;

namespace QueueVisualizer.Managers;

public class QueueManager
{
    private readonly int _maxQueueSize;
    private readonly LinkedList<ProductItem> _productQueue = new LinkedList<ProductItem>();

    public QueueManager(int maxQueueSize = 5)
    {
        _maxQueueSize = maxQueueSize;
    }

    public IEnumerable<ProductItem> ProductQueue => _productQueue;

    public bool AddProduct(string type)
    {
        if (_productQueue.Count >= _maxQueueSize) return false;

        var color = type == "good" ? Colors.Green : Colors.Yellow;
        _productQueue.AddFirst(new ProductItem(color));
        return true;
    }

    public bool RemoveProduct()
    {
        if (_productQueue.Count == 0) return false;

        _productQueue.RemoveLast();
        return true;
    }
}
