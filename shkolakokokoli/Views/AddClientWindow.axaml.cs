using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;
using shkolakokokoli.ViewModels;

namespace shkolakokokoli.Views;

public partial class AddClientWindow : Window
{
 
    public AddClientWindow()
    {
        InitializeComponent();
        
        addButton.Click += delegate { AddClient(); }; 
        cancelButton.Click += delegate { Close(null); }; 
    }

    public AddClientWindow(Client client)
    {
        InitializeComponent();

        windowText.Text = "Изменение ученика";
        addButtonText.Text = "Изменить";
        addButton.Click += delegate { ChangeClient(client.id); };
        cancelButton.Click += delegate { Close(null); };

        firstNameText.Text = client.firstName;
        surNameText.Text = client.surName;
        phoneText.Text = client.phone.ToString();
        datePicker.SelectedDate = client.birthday;
        lastLangText.Text = client.lastLanguage;
        langLevelText.Text = client.languageLevel;
        langNeedsText.Text = client.languageNeeds;
    }

    private void ChangeClient(int id)
    {
        Client client = GetData();
        client.id = id;
        if (client == null)
        {
            return;
        }
        
        Db.ChangeClient(client);
        Close(null);
    }

    private void AddClient()
    {
        Client client = GetData();
        
        if (client == null) 
        {
            return;
        }
        
        Db.AddClient(client);
        Close(null);
    }
    
    private Client GetData()
    {
        Client client = new Client();
        if (firstNameText.Text == string.Empty || surNameText.Text == string.Empty || phoneText.Text == string.Empty || datePicker.SelectedDate == null)
        {
            return null;
        }

        client.firstName = firstNameText.Text;
        client.surName = surNameText.Text;
        client.phone = Convert.ToInt32(phoneText.Text);
        DateTime? bd = datePicker.SelectedDate;
        client.birthday = (DateTime)bd;
        client.lastLanguage = lastLangText.Text;
        client.languageLevel = langLevelText.Text;
        client.languageNeeds = langNeedsText.Text;
        return client;
    }
}