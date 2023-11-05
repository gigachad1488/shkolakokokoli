using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DynamicData;
using NP.Utilities;
using shkolakokokoli.Models;
using shkolakokokoli.ViewModels;

namespace shkolakokokoli.Views;

public partial class AddClassWindow : Window
{
    private ObservableCollection<Client> addedClients { get; set; } = new ObservableCollection<Client>();
    private List<Client> clientsToAdd { get; set; } = new List<Client>();

    private Client selectedClient;
    private Client selectedAddClient;
    public AddClassWindow()
    {
        InitializeComponent();
        
        addButton.Click += delegate { AddClass(); }; 
        cancelButton.Click += delegate { Close(null); };
        addClientButton.Click += delegate { AddClient(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        addClientPopupButton.Click += delegate { SwitchPopup(); };
        datagrid.AutoGeneratingColumn += SetClientsGridCollumnName;
        addclientgrid.AutoGeneratingColumn += SetClientsGridCollumnName;
        datagrid.SelectionChanged += ClientsDataGrid_OnSelectionChanged;
        addclientgrid.SelectionChanged += AddClientsDataGrid_OnSelectionChanged;

        clientsToAdd.AddRange(MainWindowViewModel.Clients);

        datagrid.ItemsSource = addedClients;
        addclientgrid.ItemsSource = clientsToAdd;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        rootGrid.Opacity = 1;
    }

    public AddClassWindow(Class group)
    {
        InitializeComponent();

        windowText.Text = "Изменение группы";
        addButtonText.Text = "Изменить";
        addButton.Click += delegate { ChangeClass(group.id); };
        cancelButton.Click += delegate { Close(null); };
        addClientButton.Click += delegate { AddClient(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        addClientPopupButton.Click += delegate { SwitchPopup(); };
        datagrid.AutoGeneratingColumn += SetClientsGridCollumnName;
        addclientgrid.AutoGeneratingColumn += SetClientsGridCollumnName;
        datagrid.SelectionChanged += ClientsDataGrid_OnSelectionChanged;
        addclientgrid.SelectionChanged += AddClientsDataGrid_OnSelectionChanged;

        nameText.Text = group.name;
        placesText.Text = group.places.ToString();
        
        courseBox.Items.CollectionChanged += delegate
        {
            for (int i = 0; i < courseBox.Items.Count; i++)
            {
                Course c = courseBox.Items[i] as Course;
                if (c.id == group.course.id)
                {
                    courseBox.SelectedIndex = i;
                    return;
                }
            }
        };

        addedClients.AddRange(group.clients);
        clientsToAdd.AddRange(MainWindowViewModel.Clients);
        
        for (int i = 0; i < addedClients.Count; i++)
        {
            clientsToAdd.RemoveAll(x => x.id == addedClients[i].id);
        }

        datagrid.ItemsSource = addedClients;
        addclientgrid.ItemsSource = clientsToAdd;
    }

    public void SwitchPopup()
    {
        if (clientsPopup.IsOpen) 
        {
            clientsPopup.IsOpen = false;
            addclientgrid.SelectedIndex = -1;
            selectedAddClient = null;
        }
        else
        {
            clientsPopup.IsOpen = true;
            addclientgrid.SelectedIndex = -1;
            selectedAddClient = null;
        }
    }

    public void SwitchPopup(bool state)
    {
        clientsPopup.IsOpen = state;
    }

    public void AddClient()
    {       
        if (selectedAddClient != null)
        {
            Debug.WriteLine("TRIGGERED = " + selectedAddClient == null);
            addedClients.Add(selectedAddClient);
            clientsToAdd.Remove(selectedAddClient);
            selectedAddClient = null;
            SwitchPopup(false);
        }
    }

    public void DeleteClient()
    {
        if (selectedClient != null)
        {
            clientsToAdd.Add(selectedClient);
            addedClients.Remove(selectedClient);
            selectedClient = null;
            datagrid.SelectedIndex = -1;
        }
    }

    private void ChangeClass(int id)
    {
        Class group = GetData();
        if (group == null)
        {
            return;
        }
        
        group.id = id;
        Db.ChangeClass(group);
        
        Close(null);
    }

    private void AddClass()
    {
        Class group = GetData();
        
        if (group == null) 
        {
            return;
        }
        
        Db.AddClass(group);
        Close(null);
    }
    
    private Class GetData()
    {
        Class group = new Models.Class();
        int places = 0;
        if (nameText.Text == string.Empty || !Int32.TryParse(placesText.Text, out places))
        {
            return null;
        }

        group.name = nameText.Text;
        group.places = Convert.ToInt32(placesText.Text);
        group.course = (Course)courseBox.SelectedItem;
        group.clients = addedClients.ToList();
        
        return group;
    }

    private void ClientsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedClient = e.AddedItems[0] as Client;
            Debug.WriteLine("CHANGEDD");
        }
    }

    private void AddClientsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedAddClient = e.AddedItems[0] as Client;
            Debug.WriteLine("ADDCHANGEDD");
        }
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
            case "Birthday":
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
}