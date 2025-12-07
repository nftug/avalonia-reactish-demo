using HelloAvalonia.ViewModels.Contexts;
using HelloAvalonia.ViewModels.Shared;
using R3;

namespace HelloAvalonia.ViewModels;

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