using Avalonia.Controls;
using CSharpEmilyApp.ViewModels;

namespace CSharpEmilyApp.Views;

public partial class TimerView : UserControl
{
    public TimerView()
    {
        InitializeComponent();
        DataContext = new TimerViewModel();
    }
}