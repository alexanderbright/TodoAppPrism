using System;
using System.Collections.Generic;

namespace TodoApp.BL.Reports
{
  public static class AppReports
  {
    public const string PageTitleKey = "Title";
    public const string TrackingId = "UA-106889115-1";

    public static IReportsService Reports { get; set; }

    /// <summary>
    /// Report the specified message and isFatal.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="isFatal">If set to <c>true</c> is fatal.</param>
    public static void Report(string message, bool isFatal = false)
    {
      Reports?.Report(message, isFatal);
    }

    public static void Report(Exception exception, string label = "", IDictionary<string, string> extraData = null, bool isFatal = false)
    {
      Reports?.Report(exception, label, extraData, isFatal);
    }

    /// <summary>
    /// Tracks the page.
    /// </summary>
    /// <param name="pageName">Page name.</param>
    public static void TrackPage(string pageName, params KeyValuePair<string, string>[] parameters)
    {
     Reports?.TrackPage(pageName, parameters); 
    }
  }
}