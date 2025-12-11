using HelloAvalonia.Framework.Abstractions;
using R3;

namespace HelloAvalonia.Framework.Models;

public class NavigationContext : BindableBase
{
    private readonly ReactiveProperty<string> _currentPath;
    private readonly List<Func<CancellationToken, Task<bool>>> _guards = [];

    public ReadOnlyReactiveProperty<string> CurrentPath => _currentPath;

    public NavigationContext(string initialPath)
    {
        _currentPath = new ReactiveProperty<string>(initialPath).AddTo(Disposable);
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
