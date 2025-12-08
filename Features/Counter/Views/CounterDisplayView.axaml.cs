using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Features.Counter.Views;

public partial class CounterDisplayView : UserControlBase<CounterDisplayViewModel>
{
    public CounterDisplayView()
    {
        InitializeComponent();
    }
}
