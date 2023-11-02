using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
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

    public AddCourseWindow(Course course)
    {
        InitializeComponent();

        windowText.Text = "Изменение курса";
        addButtonText.Text = "Изменить";
        addButton.Click += delegate { ChangeCourse(course.id); };
        cancelButton.Click += delegate { Close(null); };

        teachersBox.Items.CollectionChanged += delegate { teachersBox.SelectedIndex = MainWindowViewModel.Teachers.Select(x => x.id).First(x => x == course.teacher.id); };
        Console.Write("TITER = " + MainWindowViewModel.Teachers.Select(x => x.id).First(x => x == course.teacher.id));
        languagesBox.Items.CollectionChanged += delegate { languagesBox.SelectedItem = course.language; };

        nameText.Text = course.name;
        priceText.Text = course.price.ToString();
    }

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
        
        if (nameText.Text == string.Empty || priceText.Text == string.Empty || !Int32.TryParse(priceText.Text, out price) || teachersBox.SelectedIndex == -1 || languagesBox.SelectedIndex == -1)
        {
            return null;
        }

        course.name = nameText.Text;
        course.teacher = (Teacher)teachersBox.SelectedItem;
        course.language = (Language)languagesBox.SelectedItem;
        course.price = price;
        
        return course;
    }
}