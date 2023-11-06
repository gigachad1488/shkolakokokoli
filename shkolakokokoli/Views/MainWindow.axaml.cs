using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using shkolakokokoli.Models;
using System;
using System.Collections.Generic;
using shkolakokokoli.ViewModels;
using Avalonia.Collections;
using System.Diagnostics;
using DynamicData;
using System.Linq;
using Avalonia.Extensions.Controls;

namespace shkolakokokoli.Views;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();

        SetClientsGrid();
        SetTeachersGrid();
        SetLanguagesGrid();
        SetCoursesGrid();
        SetClassGrid();
        SetLessonsGrid();
    }
    #region Clients

    private Client selectedClient;

    public void SetClientsGrid()
    {
        addClientButton.Click += delegate { ShowAddClientWindow(); };
        redactClientButton.Click += delegate { ShowRedactClientWindow(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        clearClientsFilterButton.Click += delegate { clientFilterText.Clear(); };

        clientsDataGrid.SelectionChanged += ClientsDataGrid_OnSelectionChanged;
        clientsDataGrid.AutoGeneratingColumn += SetClientsGridCollumnName;

        MainWindowViewModel.RefreshClients();

        clientFilterText.TextChanged += delegate { OnClientFilterChanged(); };

        MainWindowViewModel.ClientsView = new DataGridCollectionView(MainWindowViewModel.Clients);
        MainWindowViewModel.ClientsView.Filter = ClientsFilter;
        MainWindowViewModel.ClientsView.Refresh();
    }

    public void ShowAddClientWindow()
    {
        AddClientWindow adw = new AddClientWindow();
        adw.Closed += delegate { RefreshClient(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactClientWindow()
    {
        int id = clientsDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddClientWindow adw = new AddClientWindow(selectedClient);
            Console.WriteLine("ID = " + selectedClient.id);
            adw.Closed += delegate { RefreshClient(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshClient()
    {
        MainWindowViewModel.RefreshClients();
    }

    public async void DeleteClient()
    {
        int id = clientsDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteClient(selectedClient);
                RefreshClient();
            }
            Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnClientFilterChanged()
    {
        MainWindowViewModel.ClientsView.Refresh();
    }

    public void SetClientsGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "firstName":
                e.Column.Header = "Имя";
                break;
            case "surName":
                e.Column.Header = "Фамилия";
                break;
            case "phone":
                e.Column.Header = "Телефон";
                break;
            case "Birthday":
                e.Column.Header = "День рождения";
                break;
            case "lastLanguage":
                e.Column.Header = "Последний язык";
                break;
            case "languageLevel":
                e.Column.Header = "Уровень языка";
                break;
            case "languageNeeds":
                e.Column.Header = "Потребности";
                break;
        }
    }

    public bool ClientsFilter(object o)
    {
        if (clientFilterText.Text != null && clientFilterText.Text != string.Empty)
        {
            Client client = (Client)o;
            if (client.firstName.Contains(clientFilterText.Text) || client.surName.Contains(clientFilterText.Text) || client.phone.ToString().Contains(clientFilterText.Text) || client.birthday.ToString().Contains(clientFilterText.Text) || client.lastLanguage.Contains(clientFilterText.Text) || client.languageNeeds.Contains(clientFilterText.Text) || client.languageLevel.Contains(clientFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void ClientsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedClient = e.AddedItems[0] as Client;
        }
    }
    #endregion

    #region Teachers

    private Teacher selectedTeacher;
    public void SetTeachersGrid()
    {
        addTeacherButton.Click += delegate { ShowAddTeachersWindow(); };
        redactTeacherButton.Click += delegate { ShowRedactTeacherWindow(); };
        deleteTeacherButton.Click += delegate { DeleteTeacher(); };
        clearTeacherFilterButton.Click += delegate { teacherFilterText.Clear(); };

        teachersDataGrid.SelectionChanged += TeachersDataGrid_OnSelectionChanged;
        teachersDataGrid.AutoGeneratingColumn += SetTeachersGridCollumnName;

        MainWindowViewModel.RefreshTeachers();

        teacherFilterText.TextChanged += delegate { OnTeacherFilterChanged(); };

        MainWindowViewModel.TeachersView = new DataGridCollectionView(MainWindowViewModel.Teachers);
        MainWindowViewModel.TeachersView.Filter = TeachersFilter;
        MainWindowViewModel.TeachersView.Refresh();
    }

    public void ShowAddTeachersWindow()
    {
        AddTeacherWindow adw = new AddTeacherWindow();
        adw.Closed += delegate { RefreshTeacher(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactTeacherWindow()
    {
        int id = teachersDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddTeacherWindow adw = new AddTeacherWindow(selectedTeacher);
            Console.WriteLine("ID = " + selectedTeacher.id);
            adw.Closed += delegate { RefreshTeacher(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshTeacher()
    {
        MainWindowViewModel.RefreshTeachers();
    }

    public async void DeleteTeacher()
    {
        int id = teachersDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteTeacher(selectedTeacher);
                RefreshTeacher();
            }
            Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnTeacherFilterChanged()
    {
        MainWindowViewModel.TeachersView.Refresh();
    }

    public void SetTeachersGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "firstName":
                e.Column.Header = "Имя";
                break;
            case "surName":
                e.Column.Header = "Фамилия";
                break;
        }
    }

    public bool TeachersFilter(object o)
    {
        if (teacherFilterText.Text != null && teacherFilterText.Text != string.Empty)
        {
            Teacher teacher = (Teacher)o;
            if (teacher.firstName.Contains(teacherFilterText.Text) || teacher.surName.Contains(teacherFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void TeachersDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedTeacher = e.AddedItems[0] as Teacher;
        }
    }

    #endregion

    #region Language

    private Language selectedLanguage;
    public void SetLanguagesGrid()
    {
        addLanguageButton.Click += delegate { ShowAddLanguagesWindow(); };
        redactLanguageButton.Click += delegate { ShowRedactLanguagesWindow(); };
        deleteLanguageButton.Click += delegate { DeleteLanguage(); };
        clearLanguageFilterButton.Click += delegate { languageFilterText.Clear(); };

        languagesDataGrid.SelectionChanged += LanguagesDataGrid_OnSelectionChanged;
        languagesDataGrid.AutoGeneratingColumn += SetLanguagesGridCollumnName;

        MainWindowViewModel.RefreshLanguages();

        languageFilterText.TextChanged += delegate { OnLanguageFilterChanged(); };

        MainWindowViewModel.LanguagesView = new DataGridCollectionView(MainWindowViewModel.Languages);
        MainWindowViewModel.LanguagesView.Filter = LanguagesFilter;
        MainWindowViewModel.LanguagesView.Refresh();
    }

    public void ShowAddLanguagesWindow()
    {
        AddLanguageWindow adw = new AddLanguageWindow();
        adw.Closed += delegate { RefreshLanguage(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactLanguagesWindow()
    {
        int id = languagesDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddLanguageWindow adw = new AddLanguageWindow(selectedLanguage);
            Console.WriteLine("ID = " + selectedLanguage.id);
            adw.Closed += delegate { RefreshLanguage(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshLanguage()
    {
        MainWindowViewModel.RefreshLanguages();
    }

    public async void DeleteLanguage()
    {
        int id = languagesDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteLanguage(selectedLanguage);
                RefreshLanguage();
            }
            Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnLanguageFilterChanged()
    {
        MainWindowViewModel.LanguagesView.Refresh();
    }

    public void SetLanguagesGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "name":
                e.Column.Header = "Название";
                break;
        }
    }

    public bool LanguagesFilter(object o)
    {
        if (languageFilterText.Text != null && languageFilterText.Text != string.Empty)
        {
            Language language = (Language)o;
            if (language.name.Contains(languageFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void LanguagesDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedLanguage = e.AddedItems[0] as Language;
        }
    }

    #endregion

    #region Courses

    private Course selectedCourse;
    public void SetCoursesGrid()
    {
        addCourseButton.Click += delegate { ShowAddCourseWindow(); };
        redactCourseButton.Click += delegate { ShowRedactCourseWindow(); };
        deleteCourseButton.Click += delegate { DeleteCourse(); };
        clearCoursesFilterButton.Click += delegate { courseFilterText.Clear(); };

        coursesDataGrid.SelectionChanged += CoursesDataGrid_OnSelectionChanged;
        coursesDataGrid.AutoGeneratingColumn += SetCoursesGridCollumnName;

        MainWindowViewModel.RefreshCourses();

        courseFilterText.TextChanged += delegate { OnCourseFilterChanged(); };

        MainWindowViewModel.CoursesView = new DataGridCollectionView(MainWindowViewModel.Courses);
        MainWindowViewModel.CoursesView.Filter = CoursesFilter;
        MainWindowViewModel.CoursesView.Refresh();
    }

    public void ShowAddCourseWindow()
    {
        AddCourseWindow adw = new AddCourseWindow();
        adw.DataContext = this.DataContext;
        adw.Closed += delegate { RefreshCourse(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactCourseWindow()
    {
        int id = coursesDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddCourseWindow adw = new AddCourseWindow(selectedCourse);
            adw.DataContext = this.DataContext;
            adw.Closed += delegate { RefreshCourse(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshCourse()
    {
        MainWindowViewModel.RefreshCourses();
        RefreshClassFilter();
    }

    public async void DeleteCourse()
    {
        int id = coursesDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteCourse(selectedCourse);
                RefreshCourse();
            }
        }
    }

    private void OnCourseFilterChanged()
    {
        MainWindowViewModel.CoursesView.Refresh();
    }

    public void SetCoursesGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "name":
                e.Column.Header = "Название";
                break;
            case "Teacher":
                e.Column.Header = "Учитель";
                break;
            case "Language":
                e.Column.Header = "Язык";
                break;
            case "price":
                e.Column.Header = "Цена";
                break;
        }
    }

    public bool CoursesFilter(object o)
    {
        if (courseFilterText.Text != null && courseFilterText.Text != string.Empty)
        {
            Course course = (Course)o;
            if (course.name.Contains(courseFilterText.Text) || course.language.ToString().Contains(courseFilterText.Text) || course.teacher.ToString().Contains(courseFilterText.Text) || course.price.ToString().Contains(courseFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void CoursesDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedCourse = e.AddedItems[0] as Course;
        }
    }

    #endregion

    #region Classes

    private Class selectedClass;

    public List<Class> classCourseFilter { get; set; } = new List<Class>();
    public void SetClassGrid()
    {
        addClassButton.Click += delegate { ShowAddClassWindow(); };
        redactClassButton.Click += delegate { ShowRedactClassWindow(); };
        deleteClassButton.Click += delegate { DeleteClass(); };
        //clearClassFilterButton.Click += delegate { classFilterText.Clear(); };

        classesBox.SelectionChanged += ClassesBox_OnSelectionChanged;

        classesFilterBox.SelectionChanged += ClassesFilterBox_SelectionChanged;      

        //coursesDataGrid.AutoGeneratingColumn += SetCoursesGridCollumnName;

        RefreshClass();
        RefreshClassFilter();

        classesFilterBox.SelectedIndex = 0;

        //classFilterText.TextChanged += delegate { OnCourseFilterChanged(); };

        /*
        MainWindowViewModel.CoursesView = new DataGridCollectionView(MainWindowViewModel.Courses);
        MainWindowViewModel.CoursesView.Filter = CoursesFilter;
        MainWindowViewModel.CoursesView.Refresh();
        */
    }

    private void ClassesFilterBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            if (classesFilterBox.SelectedIndex > 0)
            {
                Course course = (classesFilterBox.SelectedItem as Course);
                classCourseFilter = MainWindowViewModel.Classes.Where(x => x.course.id == course.id).ToList();
            }
            else
            {
                classCourseFilter = MainWindowViewModel.Classes.ToList();
            }
            RefreshClass();
        }
    }

    public void ShowAddClassWindow()
    {
        AddClassWindow adw = new AddClassWindow();
        adw.DataContext = this.DataContext;
        adw.Closed += delegate { RefreshClass(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactClassWindow()
    {
        int id = classesBox.SelectedIndex;
        if (id != -1)
        {
            AddClassWindow adw = new AddClassWindow(selectedClass);
            adw.DataContext = this.DataContext;
            adw.Closed += delegate { RefreshClass(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshClass()
    {
        classesBox.Items.Clear();
        MainWindowViewModel.RefreshClasses();
        //RefreshClassFilter();
        foreach (var item in classCourseFilter)
        {
            classesBox.Items.Add(new ClassUserControl(item));
        }
    }

    public void RefreshClassFilter()
    {
        Course selcourse = classesFilterBox.SelectedItem as Course;
        classesFilterBox.Items.Clear();
        classesFilterBox.Items.Add(" ");
        foreach (var item in MainWindowViewModel.Courses)
        {
            classesFilterBox.Items.Add(item);
        }

        if (classesFilterBox.SelectedIndex >= 0)
        {
            for (int i = 0; i < classesFilterBox.Items.Count; i++)
            {
                Course c = classesFilterBox.Items[i] as Course;
                if (c.id == selcourse.id)
                {
                    classesFilterBox.SelectedIndex = i;
                    return;
                }
            }
        }
    }

    public async void DeleteClass()
    {
        int id = classesBox.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteClass(selectedClass);
                RefreshClass();
            }
        }
    }

    /*
    public void SetCoursesGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "name":
                e.Column.Header = "Название";
                break;
            case "Teacher":
                e.Column.Header = "Учитель";
                break;
            case "Language":
                e.Column.Header = "Язык";
                break;
            case "price":
                e.Column.Header = "Цена";
                break;
        }
    }
    */

    /*
    public bool CoursesFilter(object o)
    {
        if (courseFilterText.Text != null && courseFilterText.Text != string.Empty)
        {
            Class group = (Class)o;
            if (group.name.Contains(courseFilterText.Text) || group.course.ToString().Contains(courseFilterText.Text) || group.places.ToString().Contains(courseFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    */


    private void ClassesBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            ClassUserControl ctrl = e.AddedItems[0] as ClassUserControl;
            selectedClass = ctrl.group;
        }
    }

    #endregion

    #region Lessons

    private Lesson selectedLesson;
    public void SetLessonsGrid()
    {
        addLessonButton.Click += delegate { ShowAddLessonWindow(); };
        redactLessonButton.Click += delegate { ShowRedactLessonWindow(); };
        deleteLessonButton.Click += delegate { DeleteLesson(); };
        clearLessonsFilterButton.Click += delegate { lessonFilterText.Clear(); };

        lessonsDataGrid.SelectionChanged += LessonsDataGrid_OnSelectionChanged;
        lessonsDataGrid.AutoGeneratingColumn += SetLessonsGridCollumnName;

        MainWindowViewModel.RefreshLessons();

        lessonFilterText.TextChanged += delegate { OnLessonFilterChanged(); };

        MainWindowViewModel.LessonsView = new DataGridCollectionView(MainWindowViewModel.Lessons);
        MainWindowViewModel.LessonsView.Filter = LessonsFilter;
        MainWindowViewModel.LessonsView.Refresh();
    }

    public void ShowAddLessonWindow()
    {
        AddLessonWindow adw = new AddLessonWindow();
        adw.DataContext = this.DataContext;
        adw.Closed += delegate { RefreshLesson(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactLessonWindow()
    {
        int id = lessonsDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddLessonWindow adw = new AddLessonWindow(selectedLesson);
            adw.DataContext = this.DataContext;
            adw.Closed += delegate { RefreshLesson(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshLesson()
    {
        MainWindowViewModel.RefreshLessons();
    }

    public async void DeleteLesson()
    {
        int id = lessonsDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteLesson(selectedLesson);
                RefreshLesson();
            }
        }
    }

    private void OnLessonFilterChanged()
    {
        MainWindowViewModel.LessonsView.Refresh();
    }

    public void SetLessonsGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "id":
                e.Column.IsVisible = false;
                break;
            case "Group":
                e.Column.Header = "Группа";
                break;
            case "startTime":
                e.Column.Header = "Дата начала";
                break;
            case "endTime":
                e.Column.Header = "Дата конца";
                break;
        }
    }

    public bool LessonsFilter(object o)
    {
        if (lessonFilterText.Text != null && lessonFilterText.Text != string.Empty)
        {
            Lesson lesson = (Lesson)o;
            if (lesson.group.ToString().Contains(lessonFilterText.Text) || lesson.startTime.ToString().Contains(lessonFilterText.Text) || lesson.endTime.ToString().Contains(lessonFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void LessonsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedLesson = e.AddedItems[0] as Lesson;
        }
    }

    #endregion
}