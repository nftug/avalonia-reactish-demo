using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Framework.Models;

namespace HelloAvalonia.UI.Navigation.Adapters;

public class FANavigationPageFactory(INavigationPageStore pageStore) : INavigationPageFactory
{
    public Control GetPage(Type srcType) => throw new NotImplementedException();

    public Control GetPageFromObject(object target)
        => pageStore.LoadPageFromNavigation(target.ToString() ?? string.Empty);
}
