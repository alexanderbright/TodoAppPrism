using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;

namespace TodoApp.UI.Extensions
{
  public static class UnityContainerExtensions
  {
    public static IUnityContainer RegisterRoute<TView, TViewModel>(this IUnityContainer container, out Uri route, Uri baseRoute = null)
      where TView : Page where TViewModel : class
    {
      var routeName = typeof(TView).Name;
      route = GetRoute(routeName, baseRoute);
      return container.RegisterTypeForNavigation<TView, TViewModel>(routeName);
    }

    public static IUnityContainer RegisterRoute<TView>(this IUnityContainer container, out Uri route, Uri baseRoute = null)
      where TView : Page
    {
      var routeName = typeof(TView).Name;
      route = GetRoute(routeName, baseRoute);
      return container.RegisterTypeForNavigation<TView>(routeName);
    }

    private static Uri GetRoute(string routeName, Uri baseroute)
    {
      var route = new Uri(routeName, UriKind.Relative);
      return baseroute != null ? baseroute.Combine(route) : route;

    }
  }
}