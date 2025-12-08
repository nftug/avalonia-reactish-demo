using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HelloAvalonia.Shell.Composition;
using HelloAvalonia.Shell.ViewModels;
using HelloAvalonia.Shell.Views;

namespace HelloAvalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var container = new AppContainer();

        container.Run<MainWindowViewModel>(vm =>
        {
            vm.ContextProvider.InjectResolver(container);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow() { DataContext = vm };
            }
        });

        base.OnFrameworkInitializationCompleted();
    }
}
