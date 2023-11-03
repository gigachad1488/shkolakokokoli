using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views;

public partial class ClassUserControl : UserControl
{
    public Class group;
    
    public ClassUserControl(Class grp)
    {
        InitializeComponent();

        group = grp;
        nameText.Text = group.name;
        courseText.Text = group.course.ToString();
        placesText.Text = group.places.ToString();
    }
}