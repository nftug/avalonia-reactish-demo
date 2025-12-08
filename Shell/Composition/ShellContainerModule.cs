using HelloAvalonia.Shell.ViewModels;
using HelloAvalonia.UI.Navigation.Adapters;
using HelloAvalonia.UI.Navigation.ViewModels;
using StrongInject;

namespace HelloAvalonia.Shell.Composition;

[Register(typeof(NavigationPageFactory), Scope.InstancePerResolution)]
[Register(typeof(NavigationViewModel), Scope.InstancePerResolution)]
[Register(typeof(MainWindowViewModel), Scope.SingleInstance)]
public class ShellContainerModule;

public interface IShellContextContainers : IContainer<MainWindowViewModel>;
