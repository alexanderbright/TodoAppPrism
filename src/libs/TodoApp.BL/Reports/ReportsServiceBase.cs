using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.BL.Reports
{
  public abstract class ReportsServiceBase : IReportsService
  {
    private const int ExceptionDataLimit = 4000;
    public abstract void TrackPage(string pageName, params KeyValuePair<string, string>[] parameters);
    public abstract void Report(string message, bool isFatal = false);
    public void Report(Exception exception, string label, IDictionary<string, string> extraData, bool isFatal = false)
    {
      var builder = new StringBuilder("Label: ");
      builder.AppendLine(label);
      if (extraData != null && extraData.Count > 0)
      {
        builder.AppendLine("User Data:");
        foreach (var data in extraData)
        {
          builder.AppendLine($"- {data.Key} : {data.Value}");
        }
      }
      builder.AppendLine(exception.ToString());

      Report(builder.ToString(), isFatal);
    }

    public static string FormatException(Exception exception, string label = "")
    {
      var builder = new StringBuilder();

      if (!string.IsNullOrWhiteSpace(label))
        builder.AppendLine($"Label {label}");
      builder.AppendLine($"Type {exception.GetType()}");
      //builder.AppendLine($"Message {exception.Message}");
      builder.AppendLine($"{exception.StackTrace}");

      if (exception.InnerException != null)
        builder.AppendLine(FormatException(exception.InnerException));

      return builder.ToString();
    }
  }
}