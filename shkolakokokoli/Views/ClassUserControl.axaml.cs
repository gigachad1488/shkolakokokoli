using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views;

public partial class ClassUserControl : UserControl
{
    public Class group;

    public ClassUserControl()
    {
        InitializeComponent();
    }
    
    public ClassUserControl(Class grp)
    {
        InitializeComponent();
        
        group = grp;
        nameText.Text = group.name;
        courseText.Text = group.course.ToString();
        placesText.Text = group.places.ToString();
        popupButton.Click += delegate { PopupSwitch(); };
    }
    
    public void PopupSwitch()
    {
        if (clientsPopup.IsOpen)
        {
            popupButtonText.Text = "\u25bc";
            clientsPopup.IsOpen = false;
        }
        else
        {
            popupButtonText.Text = "\u25b2";  
            clientsPopup.IsOpen = true;
        }
    }
}