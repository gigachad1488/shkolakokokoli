using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views;

public partial class AddLanguageWindow : Window
{
    public AddLanguageWindow()
    {
        InitializeComponent();
        
        addButton.Click += delegate { AddLanguage(); }; 
        cancelButton.Click += delegate { Close(null); }; 
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        rootGrid.Opacity = 1;
    }

    public AddLanguageWindow(Language language)
    {
        InitializeComponent();

        windowText.Text = "Изменение языка";
        addButtonText.Text = "Изменить";
        addButton.Click += delegate { ChangeLanguage(language.id); };
        cancelButton.Click += delegate { Close(null); };

        nameText.Text = language.name;
    }

    private void ChangeLanguage(int id)
    {
        Language language = GetData();
        language.id = id;
        if (language == null)
        {
            return;
        }
        
        Db.ChangeLanguage(language);
        Close(null);
    }

    private void AddLanguage()
    {
        Language language = GetData();
        
        if (language == null) 
        {
            return;
        }
        
        Db.AddLanguage(language);
        Close(null);
    }
    
   private Language GetData()
   {
        Language language = new Language();
        if (nameText.Text == string.Empty)
      {
          return null;
       }

       language.name = nameText.Text;
       return language;
   }
}