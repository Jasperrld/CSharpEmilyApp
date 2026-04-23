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

    protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        var dateScroll = this.FindControl<ScrollViewer>("DateScroll");
        var contentScroll = this.FindControl<ScrollViewer>("ContentScroll");
        var headerScroll = this.FindControl<ScrollViewer>("HeaderScroll");

        if (dateScroll == null || contentScroll == null || headerScroll == null) return;

        bool syncing = false;

        contentScroll.ScrollChanged += (_, _) =>
        {
            if (syncing) return;
            syncing = true;
            dateScroll.Offset = dateScroll.Offset.WithY(contentScroll.Offset.Y);
            headerScroll.Offset = headerScroll.Offset.WithX(contentScroll.Offset.X);
            syncing = false;
        };
    }
}