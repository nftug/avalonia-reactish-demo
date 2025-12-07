namespace HelloAvalonia.Adapters.Contexts;

public record ContextReturn<T>(T Value, R3.CompositeDisposable Disposables) where T : class;