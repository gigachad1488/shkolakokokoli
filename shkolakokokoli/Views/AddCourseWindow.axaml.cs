using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;
using shkolakokokoli.ViewModels;

namespace shkolakokokoli.Views;

public partial class AddCourseWindow : Window
{
    public AddCourseWindow()
    {
        InitializeComponent();
        
        addButton.Click += delegate { AddCourse(); }; 
        cancelButton.Click += delegate { Close(null); }; 
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        rootGrid.Opacity = 1;
    }

    public AddCourseWindow(Course course)
    {
        InitializeComponent();

        windowText.Text = "Изменение курса";
        addButtonText.Text = "Изменить";
        addButton.Click += delegate { ChangeCourse(course.id); };
        cancelButton.Click += delegate { Close(null); };

        
        teachersBox.Items.CollectionChanged += delegate
        {
            for (int i = 0; i < teachersBox.Items.Count; i++)
            {
                Teacher c = teachersBox.Items[i] as Teacher;
                if (c.id == course.teacher.id)
                {
                    teachersBox.SelectedIndex = i;
                    return;
                }
            }
        };
        //Console.Write("TITER = " + MainWindowViewModel.Teachers.Select(x => x.id).First(x => x == course.teacher.id));
        languagesBox.Items.CollectionChanged += delegate
        {
            for (int i = 0; i < languagesBox.Items.Count; i++)
            {
                Language c = languagesBox.Items[i] as Language;
                if (c.id == course.language.id)
                {
                    languagesBox.SelectedIndex = i;
                    return;
                }
            }
        };

        nameText.Text = course.name;    }

    private void ChangeCourse(int id)
    {
        Course course = GetData();
        course.id = id;
        if (course == null)
        {
            return;
        }
        
        Db.ChangeCourse(course);
        Close(null);
    }

    private void AddCourse()
    {
        Course course = GetData();
        
        if (course == null) 
        {
            return;
        }
        
        Db.AddCourse(course);
        Close(null);
    }
    
    private Course GetData()
    {
        Course course = new Course();

        int price;
        
        if (nameText.Text == string.Empty || teachersBox.SelectedIndex == -1 || languagesBox.SelectedIndex == -1)
        {
            return null;
        }

        course.name = nameText.Text;
        course.teacher = (Teacher)teachersBox.SelectedItem;
        course.language = (Language)languagesBox.SelectedItem;
        
        return course;
    }
}