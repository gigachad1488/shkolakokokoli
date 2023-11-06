using Avalonia.Controls;
using Avalonia.Interactivity;
using shkolakokokoli.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using DynamicData;
using shkolakokokoli.ViewModels;


namespace shkolakokokoli.Views
{
    public partial class AddLessonWindow : Window
    {
        public ObservableCollection<ClientsAttendance> clat { get; set; } = new ObservableCollection<ClientsAttendance>();
        public AddLessonWindow()
        {
            InitializeComponent();

            addButton.Click += delegate { AddLesson(); };
            cancelButton.Click += delegate { Close(null); };
            
            datagrid.AutoGeneratingColumn += SetGridCollumnName;
            classesBox.SelectionChanged += delegate { ChangeClients(); };
            datagrid.ItemsSource = clat;
            
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            rootGrid.Opacity = 1;
        }

        public AddLessonWindow(Lesson lesson)
        {
            InitializeComponent();
            
            windowText.Text = "Изменение занятия";
            addButtonText.Text = "Изменить";
            addButton.Click += delegate { ChangeLesson(lesson.id); };
            cancelButton.Click += delegate { Close(null); };

            classesBox.Items.CollectionChanged += delegate
            {
                for (int i = 0; i < classesBox.Items.Count; i++)
                {
                    Class c = classesBox.Items[i] as Class;
                    if (c.id == lesson.group.id)
                    {
                        classesBox.SelectedIndex = i;
                        return;
                    }
                }
            };

            //classesBox.SelectionChanged += delegate { ChangeClients(); };
            classesBox.IsEnabled = false;

            startdatePicker.SelectedTime = lesson.startTime.TimeOfDay;
            enddatePicker.SelectedTime = lesson.endTime.TimeOfDay;
            
            
            foreach (var item in lesson.attendances)
            {
                ClientsAttendance cl = new ClientsAttendance();
                cl.client = lesson.group.clients.First(x => x.id == item.client);
                cl.attendance = item.attendance;
                //Console.WriteLine(cl.attendance);
                clat.Add(cl);
                //Console.WriteLine(clat[0].attendance);
            }

            datagrid.AutoGeneratingColumn += SetGridCollumnName;
            datagrid.ItemsSource = clat;
        }

        public void ChangeClients()
        {
            clat.Clear();
            Class cls = (Class)classesBox.SelectedItem;
            foreach (var item in cls.clients)
            {
                ClientsAttendance ca = new ClientsAttendance();
                ca.client = item;
                ca.attendance = false;
                clat.Add(ca);
            }
            
        }

        private void ChangeLesson(int id)
        {
            Lesson lesson = GetData();
            lesson.id = id;
            if (lesson == null)
            {
                return;
            }

            Db.ChangeLesson(lesson);
            Close(null);
        }

        private void AddLesson()
        {
            Lesson lesson = GetData();

            if (lesson == null)
            {
                return;
            }

            Db.AddLesson(lesson);
            Close(null);
        }

        private Lesson GetData()
        {
            Lesson lesson = new Lesson();

            if (startdatePicker.SelectedTime == null || enddatePicker.SelectedTime == null || classesBox.SelectedIndex == -1)
            {
                return null;
            }

            lesson.group = (Class)classesBox.SelectedItem;
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            lesson.startTime = (DateTime)(today + startdatePicker.SelectedTime);
            lesson.endTime = (DateTime)(today + enddatePicker.SelectedTime);

            foreach (var item in clat)
            {
                Attendance att = new Attendance();
                att.client = item.client.id;
                att.attendance = item.attendance;
                lesson.attendances.Add(att);
            }

            return lesson;
        }
        public class ClientsAttendance
        {
            public Client client;

            public string Client
            {
                get
                {
                    return client.ToString();
                }
            }
        
            public bool attendance { get; set; }
        }
        
        public void SetGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Client":
                    e.Column.Header = "Ученик";
                    break;
                case "attendance":
                    e.Column.Header = "Посещение";
                    e.Column.IsReadOnly = false;
                    break;
            }
        }
    }

    
}
