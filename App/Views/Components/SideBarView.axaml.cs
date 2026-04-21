using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using CSharpEmilyApp.ViewModels;

namespace CSharpEmilyApp.Views.Components;

public partial class SideBarView : UserControl
{
    public SideBarView()
    {
        InitializeComponent();
    }

    private MainWindowViewModel? GetVm() =>
        this.FindAncestorOfType<Window>()?.DataContext as MainWindowViewModel;

    private void SetActiveButton(Button active)
    {
        PlanningButton.Classes.Remove("nav-button-active");
        OtherButton.Classes.Remove("nav-button-active");
        TimerButton.Classes.Remove("nav-button-active");
        active.Classes.Add("nav-button-active");
    }

    private void OnToggleClick(object sender, RoutedEventArgs e)
        => GetVm()?.TogglePane();

    private void OnPlanningClick(object sender, RoutedEventArgs e)
    {
        GetVm()?.GoToPlanning();
        SetActiveButton(PlanningButton);
    }

    private void OnOtherClick(object sender, RoutedEventArgs e)
    {
        GetVm()?.OtherView();
        SetActiveButton(OtherButton);
    }

    private void OnTimerClick(object sender, RoutedEventArgs e)
    {
        GetVm()?.GoToTimer();
        SetActiveButton(TimerButton);
    }
}