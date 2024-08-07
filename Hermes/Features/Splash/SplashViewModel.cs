using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;

namespace Hermes.Features.Splash;

public partial class SplashViewModel()
    : Features.PageBase("About", MaterialIconKind.InfoOutline, 2)
{
    [ObservableProperty] private bool _dashBoardVisited;

    [RelayCommand]
    private void OpenDashboard()
    {
        DashBoardVisited = true;
    }
}