using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using shkolakokokoli.Models;
using System;
using System.Collections.Generic;
using shkolakokokoli.ViewModels;

namespace shkolakokokoli.Views;

public partial class MainWindow : Window
{
    private Client selectedClient;
    public MainWindow()
    {
        InitializeComponent();
        addClientButton.Click += delegate { ShowAddClientWindow(); };
        redactClientButton.Click += delegate { ShowRedactClientWindow(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        ClientUserControl ctrl = new ClientUserControl(new Client(0, "4", "sad", 454, DateTime.Now, "4", "3", "3"));

        //clientsBox.Children.Add(ctrl);
        MainWindowViewModel.ClientsView.Filter = o => String.IsNullOrEmpty("filterText.Text") ? true : ((Client)o).firstname.Contains("123");
    }
    

    public void ShowAddClientWindow()
    {
        AddClientWindow adw = new AddClientWindow();
        adw.Closed += delegate { RefreshClient(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactClientWindow()
    {
        int id = clientsDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddClientWindow adw = new AddClientWindow(selectedClient);
            Console.WriteLine("ID = " + selectedClient.id);
            adw.Closed += delegate { RefreshClient(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshClient()
    {
        MainWindowViewModel.RefreshClients();
    }

    public async void DeleteClient()
    {
        int id = clientsDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?",
                MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteClient(selectedClient);
                RefreshClient();
            }
        }
    }

    
    
    private void ClientsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedClient = e.AddedItems[0] as Client;
        }
    }

    private string FilterText_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        return "3";
    }
}