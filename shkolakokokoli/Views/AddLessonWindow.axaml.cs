using Avalonia.Controls;
using Avalonia.Interactivity;
using shkolakokokoli.Models;
using System;

namespace shkolakokokoli.Views
{
    public partial class AddLessonWindow : Window
    {
        public AddLessonWindow()
        {
            InitializeComponent();

            addButton.Click += delegate { AddLesson(); };
            cancelButton.Click += delegate { Close(null); };
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

            startdatePicker.SelectedTime = lesson.startTime.TimeOfDay;
            enddatePicker.SelectedTime = lesson.endTime.TimeOfDay;
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

            return lesson;
        }
    }
}
