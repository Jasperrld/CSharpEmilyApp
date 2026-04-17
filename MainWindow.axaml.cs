using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpEmilyApp.ViewModels;
using CSharpEmilyApp.Views;
 namespace CSharpEmilyApp;
 
 public partial class MainWindow : Window
 {
     
     private MainWindowViewModel _vm;
     public MainWindow()
     {
         InitializeComponent();

         // this way is better to switch between pages
         // MainContent.Content = new PlanningView();

         _vm = new MainWindowViewModel();
         DataContext = _vm;
     }
 }