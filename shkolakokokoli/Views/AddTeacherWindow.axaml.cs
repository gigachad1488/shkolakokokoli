using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views;

public partial class AddTeacherWindow : Window
{
    public AddTeacherWindow()
    {
        InitializeComponent();
        
        addButton.Click += delegate { AddTeacher(); }; 
        cancelButton.Click += delegate { Close(null); }; 
    }

    public AddTeacherWindow(Teacher teacher)
    {
        InitializeComponent();

        windowText.Text = "Изменение учителя";
        addButtonText.Text = "Изменить";
        addButton.Click += delegate { ChangeTeacher(teacher.id); };
        cancelButton.Click += delegate { Close(null); };

        firstNameText.Text = teacher.firstName;
        surNameText.Text = teacher.surName;
    }

    private void ChangeTeacher(int id)
    {
        Teacher teacher = GetData();
        teacher.id = id;
        if (teacher == null)
        {
            return;
        }
        
        Db.ChangeTeacher(teacher);
        Close(null);
    }

    private void AddTeacher()
    {
        Teacher teacher = GetData();
        
        if (teacher == null) 
        {
            return;
        }
        
        Db.AddTeacher(teacher);
        Close(null);
    }
    
    private Teacher GetData()
    {
        Teacher teacher = new Teacher();
        if (firstNameText.Text == string.Empty || surNameText.Text == string.Empty)
        {
            return null;
        }

        teacher.firstName = firstNameText.Text;
        teacher.surName = surNameText.Text;
        return teacher;
    }
    
}