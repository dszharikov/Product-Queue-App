using System.Windows;
using ControlApp.Managers;

namespace ControlApp;

public partial class MainWindow : Window
{
    private readonly ControlManager _controlManager;

    public MainWindow()
    {
        InitializeComponent();
        _controlManager = new ControlManager();
    }

    private void CameraButton_Click(object sender, RoutedEventArgs e)
    {
        var type = ProductSwitch.IsChecked == true ? "bad" : "good";
        _controlManager.SendAddProductCommand(type);
    }

    private void PusherButton_Click(object sender, RoutedEventArgs e)
    {
        _controlManager.SendRemoveProductCommand();
    }
}
