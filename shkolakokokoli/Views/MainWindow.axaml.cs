using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using shkolakokokoli.Models;
using System;
using System.Collections.Generic;
using shkolakokokoli.ViewModels;
using Avalonia.Collections;

namespace shkolakokokoli.Views;

public partial class MainWindow : Window
{
    private Client selectedClient;
    public MainWindow()
    {
        InitializeComponent();

        //clientsBox.Children.Add(ctrl);

        SetClientsGrid();
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

    private async void OnClientFilterChanged()
    {       
        MainWindowViewModel.ClientsView.Refresh();
    }

    public bool Rc(object o)
    {
        if (clientFilterText.Text != null && clientFilterText.Text != string.Empty)
        {
            Client client = (Client)o;
            if (client.firstname.Contains(clientFilterText.Text) || client.surName.Contains(clientFilterText.Text) || client.phone.ToString().Contains(clientFilterText.Text) || client.birthday.ToString().Contains(clientFilterText.Text) || client.lastLanguage.Contains(clientFilterText.Text) || client.languageNeeds.Contains(clientFilterText.Text) || client.languageLevel.Contains(clientFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void SetClientsGrid()
    {
        addClientButton.Click += delegate { ShowAddClientWindow(); };
        redactClientButton.Click += delegate { ShowRedactClientWindow(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        clearClientsFilterButton.Click += delegate { clientFilterText.Clear(); };

        MainWindowViewModel.Clients.Add(new Client(0, "as1", "ab7", 123, DateTime.Now, "yta", "b1", "fds"));
        MainWindowViewModel.Clients.Add(new Client(0, "as2", "ab8", 12334, DateTime.Now, "yta", "b1", "fds"));
        MainWindowViewModel.Clients.Add(new Client(0, "as3", "ab9", 134523, DateTime.Now, "yta", "b1", "fds"));
        MainWindowViewModel.Clients.Add(new Client(0, "as4", "ab0", 13223, DateTime.Now, "yta", "b1", "fds"));
        MainWindowViewModel.Clients.Add(new Client(0, "as5", "ab12", 1123, DateTime.Now, "yta", "b1", "fds"));
        MainWindowViewModel.Clients.Add(new Client(0, "as6", "ab56", 12003, DateTime.Now, "yta", "b1", "fds"));

        clientFilterText.TextChanged += delegate { OnClientFilterChanged(); };

        MainWindowViewModel.ClientsView = new DataGridCollectionView(MainWindowViewModel.Clients);
        MainWindowViewModel.ClientsView.Filter = Rc;
        MainWindowViewModel.ClientsView.Refresh();
    }


    private void ClientsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedClient = e.AddedItems[0] as Client;
        }
    }
}