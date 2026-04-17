using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpEmilyApp.Views;

namespace CSharpEmilyApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private UserControl _currentView;
    
    private bool _isPaneOpen = true;

    private string _activeWindow = "planning";

    public string ActiveWindow
    {
        get => _activeWindow;
        set
        {
            _activeWindow = value;
            OnPropertyChanged(nameof(ActiveWindow));
        }
    }

    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set
        {
            _isPaneOpen = value;
            OnPropertyChanged(nameof(IsPaneOpen));
        }
    }
    
    public UserControl CurrentView
    {
      get => _currentView;
      set
      {
         _currentView = value;
         OnPropertyChanged(nameof(CurrentView));
      }
    }

    public MainWindowViewModel()
    {
        CurrentView = new PlanningView();
    }

    public void GoToPlanning()
    {
        CurrentView = new PlanningView();
        ActiveWindow = "Planning";
    }

    public void GoBack()
    {
        CurrentView = new PlanningView();
    }

    public void OtherView()
    {
        CurrentView = new TestView();
        ActiveWindow = "Other";
    }

    public void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}