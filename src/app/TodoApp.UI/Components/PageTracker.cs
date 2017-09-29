using TodoApp.UI.Triggers;
using Xamarin.Forms;

namespace TodoApp.UI.Components
{
  public  static class PageTracker
  {
    public static readonly BindableProperty TrackProperty = BindableProperty.CreateAttached("Track"
                                                                                            , typeof(bool?)
                                                                                            , typeof(PageTracker)
                                                                                            , null, BindingMode.OneWay
                                                                                            , null
                                                                                            , OnShowMenuChanged);

    public static bool? GetTrack(BindableObject bindable)
    {
      return (bool?)bindable.GetValue(TrackProperty);
    }

    public static void SetTrack(BindableObject bindable, bool? value)
    {
      bindable.SetValue(TrackProperty, value);
    }

    private static void OnShowMenuChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
      var track = (bool?)newvalue;
      if (!track.HasValue || !track.Value)
        return;
      var page = bindable as Page;

      page?.Triggers.Add(new EventTrigger()
      {
        Event = nameof(Page.Appearing),
        Actions =
        {
          new PageTrackingEventTrigger()
        }
      });
    }
  }
}