using StrongInject;

namespace HelloAvalonia.Framework.Adapters.Contexts;

public interface IServiceContainerInstance
{
    void InjectResolver(object resolver);
    IOwnedContext<T> Resolve<T>() where T : notnull;
}

public class ServiceContainerInstance : IServiceContainerInstance
{
    private object? _container;

    public void InjectResolver(object resolver) => _container = resolver;

    public IOwnedContext<T> Resolve<T>() where T : notnull
    {
        if (_container == null)
            throw new InvalidOperationException("Resolver container is not injected");

        var owned = ((IContainer<T>)_container).Resolve();

        return new OwnedContext<T>(owned);
    }
}
