using System;
using System.Collections.Generic;
using Foundation;
using Google.Analytics;
using TodoApp.BL.Reports;

namespace TodoApp.iOS.Services
{
  public class GoogleAnalyticsService : ReportsServiceBase
  {
    private readonly ITracker _tracker;
    private readonly Gai _gaInstance;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Plugin.GoogleAnalytics.GoogleAnalyticsImplementation"/> class.
    /// </summary>
    public GoogleAnalyticsService(string trackingId)
    {
      if (trackingId == null)
        throw new ArgumentNullException(nameof(trackingId));

      _gaInstance = Gai.SharedInstance;
      _tracker = _gaInstance.GetTracker(trackingId);
      _gaInstance.TrackUncaughtExceptions = true;
      _gaInstance.DispatchInterval = 10;
    }

    public override void Report(string message, bool isFatal = false)
    {
      _tracker.Send(DictionaryBuilder.CreateException(message, isFatal).Build());
    }

    public override void TrackPage(string pageName, params KeyValuePair<string, string>[] parameters)
    {
      _tracker.Set(GaiConstants.ScreenName, pageName);
      var builder = DictionaryBuilder.CreateScreenView();
      foreach (var keyValuePair in parameters)
      {
        builder.Set(keyValuePair.Value, keyValuePair.Value);
      }
      _tracker.Send(builder.Build());
    }

    public void TrackEvent(string category, string action, string label, long value)
    {
      _tracker.Send(DictionaryBuilder.CreateEvent(category, action, label, new NSNumber(value)).Build());
      _gaInstance.Dispatch();
    }
  }
}