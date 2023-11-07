using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Collections;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.SKCharts;
using NP.Utilities;
using PCLUntils.IEnumerables;
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

    public static ObservableCollection<AttendanceChart> AttendanceCharts { get; set; } =
        new ObservableCollection<AttendanceChart>();
    
    public LabelVisual Title { get; set; } =
        new LabelVisual
        {
            Text = "Посещаемость",
            TextSize = 10,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
        };
    
    /*
     
     public ISeries[] Series { get; set; } =
       {
       new ColumnSeries<int>
       {
       Values = AttendanceCharts.Select(x => x.attendancesCount),
       }
       };
    
    public Axis[] XAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                Name = "Дата",
                NamePaint = new SolidColorPaint(SKColors.Black), 
                
                Labels = AttendanceCharts.Select(x => x.Date).ToList(),

                LabelsPaint = new SolidColorPaint(SKColors.Blue), 
                TextSize = 10,

                SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 2 }  
            }
        };
    
    public Axis[] YAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                Name = "Посещаемость",
                NamePaint = new SolidColorPaint(SKColors.Red), 

                LabelsPaint = new SolidColorPaint(SKColors.Green), 
                TextSize = 20,

                SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) 
                { 
                    StrokeThickness = 2, 
                    PathEffect = new DashEffect(new float[] { 3, 3 }) 
                } 
            }
        };

*/
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
        AttendanceCharts.Clear();

        HashSet<string> dates = new HashSet<string>();
        dates = Lessons.Select(x => x.shortDate).ToHashSet();
        string[] dts = dates.ToArray();

        int[] atts = new int[dts.Length];

        for (int i = 0; i < dts.Length; i++)
        {
            int count = 0;
            List<List<Attendance>> a = Lessons.Where(x => x.shortDate == dts[i]).Select(x => x.attendances).ToList();
            foreach (var item in a)
            {
                int cnt = item.Where(x => x.attendance == true).Count();
                count += cnt;
            }

            atts[i] = count;
        }
        
        for (int i = 0; i < atts.Length; i++)
        {
            AttendanceCharts.Add(new AttendanceChart(DateTime.Parse(dts[i]), atts[i]));
        }
        
    }
    
    
}