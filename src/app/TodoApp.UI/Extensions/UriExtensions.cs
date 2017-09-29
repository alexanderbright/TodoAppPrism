using System;
using System.Linq;

namespace TodoApp.UI.Extensions
{
  public static class UriExtensions
  {
    public static Uri Combine(this Uri uri, params Uri[] fragments)
    {
      return new Uri(fragments.Aggregate(uri.OriginalString, (current, path) =>
        $"{current.TrimEnd('/')}/{path.OriginalString.TrimStart('/')}"), UriKind.Relative);
    }
  }
}