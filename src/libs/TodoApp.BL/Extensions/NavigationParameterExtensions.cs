using Prism.Navigation;

namespace TodoApp.BL.Extensions
{
  public static class NavigationParameterExtensions
  {
    public static NavigationParameters SetTypedParameter<T>(this NavigationParameters parameters, T parameter)
    {
      parameters.Add(GetTypeParameterName<T>(), parameter);
      return parameters;
    }

    public static bool TryGetTypedParameter<T>(this NavigationParameters parameters
                                                , out T value)
    {
      return parameters.TryGetValue(GetTypeParameterName<T>(), out value);
    }

    public static string GetTypeParameterName<T>()
    {
      return typeof(T).FullName;
    }
  }
}