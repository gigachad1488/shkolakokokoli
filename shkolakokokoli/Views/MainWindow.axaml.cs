using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using shkolakokokoli.Models;
using System;

namespace shkolakokokoli.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        addClientButton.Click += delegate { ShowAddClientWindow(); };
        redactClientButton.Click += delegate { ShowRedactClientWindow(); };
        deleteClientButton.Click += delegate { DeleteClient(0); };

        ClientUserControl ctrl = new ClientUserControl(new Client(0, "4", "sad", 454, DateTime.Now, "4", "3", "3"));
        clientsBox.Items.Add(ctrl);

    }

    public void ShowAddClientWindow()
    {
        AddClientWindow adw = new AddClientWindow();
        adw.ShowDialog(this);
    }

    public void ShowRedactClientWindow()
    {
        AddClientWindow adw = new AddClientWindow(new Client(0, "4", "sad", 454, DateTime.Now, "4", "3", "3"));
        adw.ShowDialog(this);
    }

    public async void DeleteClient(int id)
    {
        var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
        var result = await mBox.ShowAsPopupAsync(this);

        if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
        {
            
        }
    }
}