using Avalonia.Controls;
using CSharpEmilyApp.ViewModels;

namespace CSharpEmilyApp.Views.Components;

public partial class PlanningHeader : UserControl
{
    public PlanningHeader()
    {
        InitializeComponent();
        DataContext = new PlanningViewModel();
    }
}