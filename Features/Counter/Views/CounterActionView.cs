using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Features.Counter.Views;

public partial class CounterActionView : UserControlBase<CounterActionViewModel>
{
    public CounterActionView()
    {
        InitializeComponent();
    }
}
