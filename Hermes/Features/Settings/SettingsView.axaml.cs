using Avalonia;
using Avalonia.Controls;
using ConfigFactory;
using ConfigFactory.Avalonia;
using ConfigFactory.Models;
using Avalonia.Markup.Xaml;
using Hermes.Models;

namespace Hermes;

public partial class SettingsView : Window
{
    public SettingsView()
    {
        InitializeComponent();
        if (ConfigPage.DataContext is ConfigPageModel model)
        {
            model.Append<Settings>();
        }
    }
}