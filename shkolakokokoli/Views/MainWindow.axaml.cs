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
    
    public MainWindow()
    {
        InitializeComponent();

        SetClientsGrid();
        SetTeachersGrid();
        SetLanguagesGrid();

    }
    bool bd = false;
    #region Clients

    private Client selectedClient;
    

    public void SetClientsGrid()
    {
        addClientButton.Click += delegate { ShowAddClientWindow(); };
        redactClientButton.Click += delegate { ShowRedactClientWindow(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        clearClientsFilterButton.Click += delegate { clientFilterText.Clear(); };

        clientsDataGrid.SelectionChanged += ClientsDataGrid_OnSelectionChanged;
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

    #region Teachers

    private Teacher selectedTeacher;
    public void SetTeachersGrid()
    {
        addTeacherButton.Click += delegate { ShowAddTeachersWindow(); };
        redactTeacherButton.Click += delegate { ShowRedactTeacherWindow(); };
        deleteTeacherButton.Click += delegate { DeleteTeacher(); };
        clearTeacherFilterButton.Click += delegate { teacherFilterText.Clear(); };

        teachersDataGrid.SelectionChanged += TeachersDataGrid_OnSelectionChanged;
        teachersDataGrid.AutoGeneratingColumn += SetTeachersGridCollumnName;

        if (bd)
        { 
            MainWindowViewModel.RefreshTeachers();
        }
        else
        {
            MainWindowViewModel.Teachers.Add(new Teacher(0, "as1", "ab7"));
            MainWindowViewModel.Teachers.Add(new Teacher(0, "as2", "ab8"));
            MainWindowViewModel.Teachers.Add(new Teacher(0, "as3", "ab2"));
            MainWindowViewModel.Teachers.Add(new Teacher(0, "as4", "ab3"));
            MainWindowViewModel.Teachers.Add(new Teacher(0, "as5", "ab71"));
        }

        teacherFilterText.TextChanged += delegate { OnTeacherFilterChanged(); };

        MainWindowViewModel.TeachersView = new DataGridCollectionView(MainWindowViewModel.Teachers);
        MainWindowViewModel.TeachersView.Filter = TeachersFilter;
        MainWindowViewModel.TeachersView.Refresh();
    }

    public void ShowAddTeachersWindow()
    {
        AddTeacherWindow adw = new AddTeacherWindow();
        adw.Closed += delegate { RefreshTeacher(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactTeacherWindow()
    {
        int id = teachersDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddTeacherWindow adw = new AddTeacherWindow(selectedTeacher);
            Console.WriteLine("ID = " + selectedTeacher.id);
            adw.Closed += delegate { RefreshTeacher(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshTeacher()
    {
        MainWindowViewModel.RefreshTeachers();
    }

    public async void DeleteTeacher()
    {
        int id = teachersDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteTeacher(selectedTeacher);
                RefreshTeacher();
            }
            Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnTeacherFilterChanged()
    {       
        MainWindowViewModel.TeachersView.Refresh();
    }

    public void SetTeachersGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
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
        }
    }

    public bool TeachersFilter(object o)
    {
        if (teacherFilterText.Text != null && teacherFilterText.Text != string.Empty)
        {
            Teacher teacher = (Teacher)o;
            if (teacher.firstName.Contains(teacherFilterText.Text) || teacher.surName.Contains(teacherFilterText.Text))
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

    private void TeachersDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedTeacher = e.AddedItems[0] as Teacher;
        }
    }
    

    #endregion

    #region Language

    private Language selectedLanguage;
    public void SetLanguagesGrid()
    {
        addLanguageButton.Click += delegate { ShowAddLanguagesWindow(); };
        redactLanguageButton.Click += delegate { ShowRedactLanguagesWindow(); };
        deleteLanguageButton.Click += delegate { DeleteLanguage(); };
        clearLanguageFilterButton.Click += delegate { languageFilterText.Clear(); };

        languagesDataGrid.SelectionChanged += LanguagesDataGrid_OnSelectionChanged;
        languagesDataGrid.AutoGeneratingColumn += SetLanguagesGridCollumnName;

        if (bd)
        { 
            MainWindowViewModel.RefreshLanguages();
        }
        else
        {
            MainWindowViewModel.Languages.Add(new Language(0, "as1"));
            MainWindowViewModel.Languages.Add(new Language(0, "as4"));
            MainWindowViewModel.Languages.Add(new Language(0, "as3"));
            MainWindowViewModel.Languages.Add(new Language(0, "as2"));
            MainWindowViewModel.Languages.Add(new Language(0, "as5"));
        }

        languageFilterText.TextChanged += delegate { OnLanguageFilterChanged(); };

        MainWindowViewModel.LanguagesView = new DataGridCollectionView(MainWindowViewModel.Languages);
        MainWindowViewModel.LanguagesView.Filter = LanguagesFilter;
        MainWindowViewModel.LanguagesView.Refresh();
    }

    public void ShowAddLanguagesWindow()
    {
        AddLanguageWindow adw = new AddLanguageWindow();
        adw.Closed += delegate { RefreshLanguage(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactLanguagesWindow()
    {
        int id = languagesDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddLanguageWindow adw = new AddLanguageWindow(selectedLanguage);
            Console.WriteLine("ID = " + selectedLanguage.id);
            adw.Closed += delegate { RefreshLanguage(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshLanguage()
    {
        MainWindowViewModel.RefreshLanguages();
    }

    public async void DeleteLanguage()
    {
        int id = languagesDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteLanguage(selectedLanguage);
                RefreshLanguage();
            }
            Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnLanguageFilterChanged()
    {       
        MainWindowViewModel.LanguagesView.Refresh();
    }

    public void SetLanguagesGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "name":
                e.Column.Header = "Название";
                break;
        }
    }

    public bool LanguagesFilter(object o)
    {
        if (languageFilterText.Text != null && languageFilterText.Text != string.Empty)
        {
            Language language = (Language)o;
            if (language.name.Contains(languageFilterText.Text))
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

    private void LanguagesDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedLanguage = e.AddedItems[0] as Language;
        }
    }

    #endregion

    #region Classes

    

    #endregion
}