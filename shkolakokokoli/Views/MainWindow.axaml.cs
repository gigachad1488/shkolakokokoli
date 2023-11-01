using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using shkolakokokoli.Models;
using System;
using System.Collections.Generic;
using shkolakokokoli.ViewModels;
using Avalonia.Collections;
using System.Diagnostics;
using DynamicData;

namespace shkolakokokoli.Views;

public partial class MainWindow : Window
{
    private Client selectedClient;
    public MainWindow()
    {
        InitializeComponent();

        SetClientsGrid();
    }

    #region Clients

    bool bd = false;

    public void SetClientsGrid()
    {
        addClientButton.Click += delegate { ShowAddClientWindow(); };
        redactClientButton.Click += delegate { ShowRedactClientWindow(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        clearClientsFilterButton.Click += delegate { clientFilterText.Clear(); };

        clientsDataGrid.AutoGeneratingColumn += SetClientsGridCollumnName;

        if (bd)
        { 
            MainWindowViewModel.RefreshClients();
        }
        else
        {
            MainWindowViewModel.Clients.Add(new Client(0, "as1", "ab7", 123, DateTime.Now, "yta", "b1", "fds"));
            MainWindowViewModel.Clients.Add(new Client(0, "as2", "ab8", 12334, DateTime.Now, "yta", "b1", "fds"));
            MainWindowViewModel.Clients.Add(new Client(0, "as3", "ab9", 134523, DateTime.Now, "yta", "b1", "fds"));
            MainWindowViewModel.Clients.Add(new Client(0, "as4", "ab0", 13223, DateTime.Now, "yta", "b1", "fds"));
            MainWindowViewModel.Clients.Add(new Client(0, "as5", "ab12", 1123, DateTime.Now, "yta", "b1", "fds"));
            MainWindowViewModel.Clients.Add(new Client(0, "as6", "ab56", 12003, DateTime.Now, "yta", "b1", "fds"));
        }

        clientFilterText.TextChanged += delegate { OnClientFilterChanged(); };

        MainWindowViewModel.ClientsView = new DataGridCollectionView(MainWindowViewModel.Clients);
        MainWindowViewModel.ClientsView.Filter = ClientsFilter;
        MainWindowViewModel.ClientsView.Refresh();
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
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteClient(selectedClient);
                RefreshClient();
            }
            Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnClientFilterChanged()
    {       
        MainWindowViewModel.ClientsView.Refresh();
    }

    public void SetClientsGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "firstName":
                e.Column.Header = "Имя";
                break;
            case "surName":
                e.Column.Header = "Фамилия";
                break;
            case "phone":
                e.Column.Header = "Телефон";
                break;
            case "birthday":
                e.Column.Header = "День рождения";
                break;
            case "lastLanguage":
                e.Column.Header = "Последний язык";
                break;
            case "languageLevel":
                e.Column.Header = "Уровень языка";
                break;
            case "languageNeeds":
                e.Column.Header = "Потребности";
                break;          
        }
    }

    public bool ClientsFilter(object o)
    {
        if (clientFilterText.Text != null && clientFilterText.Text != string.Empty)
        {
            Client client = (Client)o;
            if (client.firstName.Contains(clientFilterText.Text) || client.surName.Contains(clientFilterText.Text) || client.phone.ToString().Contains(clientFilterText.Text) || client.birthday.ToString().Contains(clientFilterText.Text) || client.lastLanguage.Contains(clientFilterText.Text) || client.languageNeeds.Contains(clientFilterText.Text) || client.languageLevel.Contains(clientFilterText.Text))
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

    private void ClientsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedClient = e.AddedItems[0] as Client;
        }
    }
    #endregion
}