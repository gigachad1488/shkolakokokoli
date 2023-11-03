using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views;

public partial class AddClassWindow : Window
{
    public AddClassWindow()
    {
        InitializeComponent();
        
        addButton.Click += delegate { AddTeacher(); }; 
        cancelButton.Click += delegate { Close(null); }; 
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
        addButton.Click += delegate { ChangeTeacher(teacher.id); };
        cancelButton.Click += delegate { Close(null); };

        nameText.Text = group.name;
        placesText.Text = group.places.ToString();
        courseText.Text = group.course.ToString();
    }

    private void ChangeClass(int id)
    {
        Class group = GetData();
        group.id = id;
        if (group == null)
        {
            return;
        }
        
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
    
    private Teacher GetData()
    {
        Class group = new Class();
        int places = 0;
        if (nameText.Text == string.Empty || Int32.TryParse(placesText.Text, out places))
        {
            return null;
        }

        group.name = nameText.Text;
        teacher.surName = surNameText.Text;
        return teacher;
    }
}