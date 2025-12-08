using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Features.Counter.ViewModels;
using StrongInject;

namespace HelloAvalonia.Shell.Composition;

[Register(typeof(CounterContext), Scope.SingleInstance)]
[Register(typeof(CounterPageViewModel), Scope.InstancePerResolution)]
[Register(typeof(CounterActionViewModel), Scope.InstancePerResolution)]
[Register(typeof(CounterDisplayViewModel), Scope.InstancePerResolution)]
public class CounterContainerModule;

public interface ICounterContainers : IContainer<CounterPageViewModel>;
