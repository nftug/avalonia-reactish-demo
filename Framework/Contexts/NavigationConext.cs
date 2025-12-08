using R3;

namespace HelloAvalonia.Framework.Contexts;

public class NavigationContext : ContextBase
{
    public IReadOnlyList<string> Paths { get; }
    public ReadOnlyReactiveProperty<string> CurrentPath { get; }

    private readonly ReactiveProperty<string> _currentPath;
    private readonly List<Func<CancellationToken, Task<bool>>> _guards = [];

    public NavigationContext(IEnumerable<string> paths, string initialPath)
    {
        Paths = paths.ToList().AsReadOnly();

        _currentPath = new ReactiveProperty<string>(initialPath).AddTo(Disposable);

        CurrentPath = _currentPath
            .Select(path => Paths.FirstOrDefault(p => p == path) ?? string.Empty)
            .ToReadOnlyReactiveProperty(string.Empty)
            .AddTo(Disposable);
    }

    public IDisposable RegisterGuard(Func<CancellationToken, Task<bool>> guard)
    {
        _guards.Add(guard);
        return R3.Disposable.Create(() => _guards.Remove(guard));
    }

    public async Task<bool> NavigateAsync(string path, CancellationToken cancellationToken = default)
    {
        if (_currentPath.Value == path) return true;

        bool canProceed = true;
        foreach (var guard in _guards)
        {
            if (canProceed && !await guard(cancellationToken))
            {
                canProceed = false;
                break;
            }
        }

        if (canProceed)
            _currentPath.Value = path;

        return canProceed;
    }
}
