namespace HelloAvalonia.Adapters.Contexts;

public record Context<T>(T Value, R3.CompositeDisposable Disposables) where T : class;