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
        
        addButton.Click += delegate { AddClass(); }; 
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
        addButton.Click += delegate { ChangeClass(group.id); };
        cancelButton.Click += delegate { Close(null); };

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
        
        return group;
    }
}