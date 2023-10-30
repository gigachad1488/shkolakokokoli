using Avalonia.Controls;
using Avalonia.Interactivity;

namespace shkolakokokoli.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        addClientButton.Click += delegate { ShowAddClientWindow(); };
    }

    public void ShowAddClientWindow()
    {
        AddClientWindow adw = new AddClientWindow();
        adw.ShowDialog(this);
    }
}