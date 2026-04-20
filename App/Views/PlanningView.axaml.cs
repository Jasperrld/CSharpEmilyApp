using Avalonia.Controls;
using CSharpEmilyApp.ViewModels;

namespace CSharpEmilyApp.Views;

public partial class PlanningView : UserControl
{
    public PlanningView()
    {
        InitializeComponent();
        DataContext = new PlanningViewModel();
    }
    
}