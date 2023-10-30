using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace shkolakokokoli.Views;

public partial class AddClientWindow : Window
{
    public AddClientWindow()
    {
        InitializeComponent();

        addButton.Click += delegate { AddClient(); }; 
        cancelButton.Click += delegate { Close(null); }; 
    }

    private void AddClient()
    {
        Client client = new Client();
        if (firstNameText.Text == string.Empty || surNameText.Text == string.Empty || phoneText.Text == string.Empty || datePicker.SelectedDate == null)
        {
            return;
        }
        
        client.firstname = firstNameText.Text;
        client.surName = surNameText.Text;
        client.phone = Convert.ToInt32(phoneText.Text);
        DateTime? bd = datePicker.SelectedDate;
        client.birthday = (DateTime)bd;
        client.lastLanguage = lastLangText.Text;
        client.languageLevel = langLevelText.Text;
        client.languageNeeds = langNeedsText.Text;
        Db.AddClient(client);
        Close(null);
    }
}