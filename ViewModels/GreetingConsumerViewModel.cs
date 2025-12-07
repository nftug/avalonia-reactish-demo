using HelloAvalonia.Adapters.Contexts;
using HelloAvalonia.ViewModels.Contexts;
using HelloAvalonia.ViewModels.Shared;

namespace HelloAvalonia.ViewModels;

public class GreetingConsumerViewModel : ViewModelBase
{
    private GreetingContext? _context;
    public GreetingContext? Context
    {
        get => _context;
        private set
        {
            _context = value;
            OnPropertyChanged();
        }
    }

    public void AttachHosts(IContextViewHost viewHost)
    {
        var context = viewHost.ResolveContext<GreetingContext>();
        Context = context?.Value;
    }
}
