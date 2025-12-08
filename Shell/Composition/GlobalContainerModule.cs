using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.Contexts;
using HelloAvalonia.UI.Adapters;
using HelloAvalonia.UI.Services;
using StrongInject;

namespace HelloAvalonia.Shell.Composition;

[Register(typeof(ServiceContainerInstance), Scope.SingleInstance, typeof(IServiceContainerInstance))]
[Register(typeof(DialogService), Scope.SingleInstance, typeof(IDialogService))]
[RegisterFactory(typeof(NavigationContextFactory), Scope.SingleInstance)]
public class GlobalContainerModule;

public class NavigationContextFactory : IFactory<NavigationContext>
{
    public NavigationContext Create() => new("/");
}
