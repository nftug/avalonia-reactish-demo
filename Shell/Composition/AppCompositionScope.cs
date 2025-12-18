using HelloAvalonia.Framework.Interfaces;

namespace HelloAvalonia.Shell.Composition;

internal sealed class AppCompositionScope(AppContainer root) : ICompositionScope
{
    private readonly AppContainer _container = new(root);

    public T Resolve<T>() => _container.Resolve<T>();

    public ICompositionScope CreateScope() => new AppCompositionScope(_container);

    public void Dispose() => _container.Dispose();
}

internal sealed class AppCompositionScopeFactory : ICompositionScopeFactory
{
    public ICompositionScope CreateScope() => new AppCompositionScope(AppDI.Instance);
}
