using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using QueueVisualizer.Managers;
using QueueVisualizer.Networking;

namespace QueueVisualizer;

public partial class MainWindow : Window
{
    private readonly QueueManager _queueManager;
    private readonly CommandReceiver _commandReceiver;

    public MainWindow()
    {
        InitializeComponent();
        _queueManager = new QueueManager();
        _commandReceiver = new CommandReceiver(_queueManager, UpdateQueueDisplay);
        _commandReceiver.StartListening();
    }

    private void UpdateQueueDisplay()
    {
        Dispatcher.Invoke(() =>
        {
            QueueDisplay.ItemsSource = null;
            QueueDisplay.ItemsSource = _queueManager.ProductQueue.ToList();
        });
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
    }
}
