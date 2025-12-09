using HelloAvalonia.Features.CounterList.ViewModels;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Features.CounterList.Views;

public partial class CounterListPage : UserControlBase<CounterListPageViewModel>
{
    public CounterListPage()
    {
        InitializeComponent();
    }
}