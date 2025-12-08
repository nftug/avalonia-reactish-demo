using StrongInject;

namespace HelloAvalonia.Shell.Composition;

[RegisterModule(typeof(GlobalContainerModule))]
[RegisterModule(typeof(ShellContainerModule))]
[RegisterModule(typeof(CounterContainerModule))]
public partial class AppContainer : IShellContextContainers, ICounterContainers;
