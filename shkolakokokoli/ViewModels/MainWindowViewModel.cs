using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Collections;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
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
    
    public static ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
    public static DataGridCollectionView CoursesView { get; set; } = new DataGridCollectionView(Courses);

    public static ObservableCollection<Lesson> Lessons { get; set; } = new ObservableCollection<Lesson>();
    public static DataGridCollectionView LessonsView { get; set; } = new DataGridCollectionView(Lessons);

    public static ObservableCollection<AttendanceChart> AttendanceChart { get; set; } = new ObservableCollection<AttendanceChart>() 
    {
        new AttendanceChart(DateTime.Now, 5),
        new AttendanceChart(new DateTime(2023, 07, 10), 15)
    };

    public ISeries[] Series { get; set; } =
    {
        new LineSeries<double>
        {
            Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
            Fill = null
        }
    };

    public LabelVisual Title { get; set; } =
        new LabelVisual
        {
            Text = "My chart title",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
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
    }

    public static void RefreshCourses()
    {
        Courses.Clear();
        Courses.AddRange(Db.GetAllCourses());
        CoursesView.Refresh();
        //Console.Write(Courses[0].name);
    }

    public static void RefreshLessons()
    {
        Lessons.Clear();
        Lessons.AddRange(Db.GetAllLessons());
        LessonsView.Refresh();
        //AttendanceChart.Clear();

        /*
        foreach (var item in Lessons)
        {
            AttendanceChart ct = new AttendanceChart();
            ct.date = new DateTime(item.startTime.Year, item.startTime.Month, item.startTime.Day);
            ct.attendancesCount = item.attendances.Where(x => x.attendance == true).Count();
            AttendanceChart.Add(ct);
        }
        */
    }
    
}