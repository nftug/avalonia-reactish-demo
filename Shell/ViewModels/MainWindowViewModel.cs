using HelloAvalonia.Features.Greeting.Contexts;
using HelloAvalonia.Features.Greeting.ViewModels;
using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.Shell.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public BindableReactiveProperty<string?> Title { get; }

    public GreetingViewModel GreetingViewModel { get; }

    public GreetingContext GreetingContext { get; } = new GreetingContext();

    public MainWindowViewModel()
    {
        GreetingViewModel =
            new GreetingViewModel(
                Observable.Return<string?>("Hello, Avalonia with MVVM!")
            );

        Title = new BindableReactiveProperty<string?>("Hello, Avalonia with MVVM!").AddTo(Disposable);

        GreetingContext.Text
            .Skip(1)
            .Subscribe(text => Title.Value = text)
            .AddTo(Disposable);
    }
}