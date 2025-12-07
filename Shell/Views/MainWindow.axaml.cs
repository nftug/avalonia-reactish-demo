using Avalonia.Controls;
using HelloAvalonia.Shell.ViewModels;

namespace HelloAvalonia.Shell.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = new MainWindowViewModel();
        InitializeComponent();
    }
}
