namespace HelloAvalonia.Framework.Interfaces;

public interface ICompositionScope : IDisposable
{
    T Resolve<T>();
    ICompositionScope CreateScope();
}

public interface ICompositionScopeFactory
{
    ICompositionScope CreateScope();
}
