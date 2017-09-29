using System;
using System.Collections.Generic;

namespace TodoApp.BL.Reports
{
  public interface IReportsService
  {
    /// <summary>
    /// Report the specified message and isFatal.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="isFatal">If set to <c>true</c> is fatal.</param>
    void Report(string message, bool isFatal = false);

    /// <summary>
    /// Tracks the page.
    /// </summary>
    /// <param name="pageName">Page name.</param>
    void TrackPage(string pageName, params KeyValuePair<string, string>[] parameters);

    void Report(Exception exception, string label = "", IDictionary<string, string> extraData = null, bool isFatal = false);
  }
}