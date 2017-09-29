using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Analytics;
using TodoApp.BL.Reports;

namespace TodoApp.Droid.Services
{
  public class GoogleAnalyticsService : ReportsServiceBase
  {
    private readonly Tracker _tracker;
    private readonly GoogleAnalytics _gaInstance;

    /// <summary>
    /// Gets or sets a value indicating whether this
    /// <see cref="T:Plugin.GoogleAnalytics.GoogleAnalyticsImplementation"/> write exceptions on device log.
    /// </summary>
    /// <value><c>true</c> if write exceptions on device log; otherwise, <c>false</c>.</value>
    public bool WriteExceptionsOnDeviceLog { get; set; } = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Plugin.GoogleAnalytics.GoogleAnalyticsImplementation"/> class.
    /// </summary>
    public GoogleAnalyticsService(string trackingId, Context appContext = null)
    {
      if (trackingId == null)
        throw new ArgumentNullException(nameof(trackingId));

      _gaInstance = GoogleAnalytics.GetInstance(appContext);
      _gaInstance.SetLocalDispatchPeriod(10);

      _tracker = _gaInstance.NewTracker(trackingId);

      _tracker.EnableExceptionReporting(true);
      _tracker.EnableAutoActivityTracking(false);
    }

    public override void TrackPage(string pageName, params KeyValuePair<string, string>[] parameters)
    {
      _tracker.SetScreenName(pageName);
      var builder = new HitBuilders.ScreenViewBuilder();
      foreach (var keyValuePair in parameters)
      {
        builder.Set(keyValuePair.Key, keyValuePair.Value);
      }
      _tracker.Send(builder.Build());
    }

    public override void Report(string message, bool isFatal = false)
    {
      if (WriteExceptionsOnDeviceLog)
        Android.Util.Log.Debug(Application.Context.PackageName, message);

      var builder = new HitBuilders.ExceptionBuilder();
      builder.SetDescription(message)
          .SetFatal(isFatal);

      _tracker.Send(builder.Build());
    }

    public void TrackEvent(string category, string action, string label, long value)
    {
      var builder = new HitBuilders.EventBuilder();
      builder.SetCategory(category);
      builder.SetAction(action);
      builder.SetLabel(label);
      builder.SetValue(value);

      _tracker.Send(builder.Build());
      _gaInstance.DispatchLocalHits();
    }
  }
}