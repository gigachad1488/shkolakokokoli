using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;
using shkolakokokoli.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using System;
using Avalonia.Collections;

namespace shkolakokokoli.Views;

public partial class ClassUserControl : UserControl
{
    public Class group;

    private ObservableCollection<Client> clients { get; set; } = new ObservableCollection<Client>();

    public ClassUserControl()
    {
        InitializeComponent();
        //nameText.Text = group.name;
        //courseText.Text = group.course.ToString();
        //placesText.Text = group.places.ToString();
        popupButton.Click += delegate { PopupSwitch(); };
    }
  
    public ClassUserControl(Class grp)
    {
        InitializeComponent();
        
        group = grp;
        nameText.Text = group.name;
        courseText.Text = group.course.ToString();
        placesText.Text = group.places.ToString();
        popupButton.Click += delegate { PopupSwitch(); };
        datagrid.AutoGeneratingColumn += SetClientsGridCollumnName;
        //LostFocus += delegate { PopupSwitch(false); };

        clients.AddRange(group.clients);

        datagrid.ItemsSource = clients;     
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

    public void PopupSwitch()
    {
        if (clientsPopup.IsOpen)
        {
            popupButtonText.Text = "\u25bc";
            clientsPopup.IsOpen = false;
        }
        else
        {
            datagrid.SelectedIndex = -1;
            popupButtonText.Text = "\u25b2";  
            clientsPopup.IsOpen = true;
        }
    }

    public void PopupSwitch(bool state)
    {
        if (state)
        {
            datagrid.SelectedIndex = -1;
            popupButtonText.Text = "\u25b2";
            clientsPopup.IsOpen = true;
        }
        else
        {
            popupButtonText.Text = "\u25bc";
            clientsPopup.IsOpen = false;
        }
    }
}