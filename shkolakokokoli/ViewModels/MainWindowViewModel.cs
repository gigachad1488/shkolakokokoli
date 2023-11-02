using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Collections;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using shkolakokokoli.Models;
using shkolakokokoli.Views;
using SkiaSharp;

namespace shkolakokokoli.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public static ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
    public static DataGridCollectionView ClientsView { get; set; } = new DataGridCollectionView(Clients);

    public static ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();
    public static DataGridCollectionView TeachersView { get; set; } = new DataGridCollectionView(Teachers);
    
    public static ObservableCollection<Language> Languages { get; set; } = new ObservableCollection<Language>();
    public static DataGridCollectionView LanguagesView { get; set; } = new DataGridCollectionView(Languages);
    
    public static ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();
    public static DataGridCollectionView ClassesView { get; set; } = new DataGridCollectionView(Classes);
    
    public static ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
    public static DataGridCollectionView CoursesView { get; set; } = new DataGridCollectionView(Courses);
    
    public ISeries[] Series { get; set; } = new ISeries[]
    {
        new LineSeries<double>
        {
            Values = new double[] {0, 4, 10, 12, 8, 2, -2},
            Fill = new SolidColorPaint(new SKColor(0, 90, 120)),
            Stroke = new SolidColorPaint(new SKColor(120, 152, 203)),
            LineSmoothness = 50
        }
    };

    public MainWindowViewModel()
    {

    }
    
    public static void RefreshClients()
    {
        Clients.Clear();
        Clients.AddRange(Db.GetAllClients());
        ClientsView.Refresh();
    }
    
    public static void RefreshTeachers()
    {
        Teachers.Clear();
        Teachers.AddRange(Db.GetAllTeachers());
        TeachersView.Refresh();
    }
    
    public static void RefreshLanguages()
    {
        Languages.Clear();
        Languages.AddRange(Db.GetAllLanguages());
        LanguagesView.Refresh();
    }
    
    public static void RefreshClasses()
    {
        Classes.Clear();
        Classes.AddRange(Db.GetAllClasses());
        ClassesView.Refresh();
    }

    public static void RefreshCourses()
    {
        Courses.Clear();
        Courses.AddRange(Db.GetAllCourses());
        CoursesView.Refresh();
        Console.Write(Courses[0].name);
    }
    
}