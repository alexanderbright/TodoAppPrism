using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using TodoApp.BL.Reports;
using UIKit;

namespace TodoApp.iOS.Utils
{
  public class CrashReporter
  {
    public static void Init()
    {
      AppDomain.CurrentDomain.FirstChanceException += CurrentDomainOnFirstChanceException;
      AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
      TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
    }

    private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
      const string label = "TaskSchedulerOnUnobservedTaskException";
      var newExc = new Exception(label, e.Exception);
      e.SetObserved();
      var aggregateExc = e.Exception;
      LogUnhandledException(newExc);
      AppReports.Report(e.Exception, label);
    }

    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      const string label = "CurrentDomainOnUnhandledException";
      var exc = e.ExceptionObject as Exception;
      var newExc = new Exception(label, exc);
      LogUnhandledException(newExc);
      AppReports.Report(exc, label, isFatal: true);
    }

    private static void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
    {
      const string label = "CurrentDomainOnUnhandledException";
      var newExc = new Exception(label, e.Exception);
      LogUnhandledException(newExc);
      AppReports.Report(e.Exception, label);
    }

    internal static void LogUnhandledException(Exception exception)
    {
      try
      {
        const string errorFileName = "Fatal.log";
        var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // iOS: Environment.SpecialFolder.Resources
        var errorFilePath = Path.Combine(libraryPath, errorFileName);
        var errorMessage = $"Time: {DateTime.Now}\r\nError: Unhandled Exception\r\n{exception.ToString()}";
        File.WriteAllText(errorFilePath, errorMessage);
      }
      catch
      {
        // just suppress any error logging exceptions
      }
    }

    /// <summary>
    // If there is an unhandled exception, the exception information is diplayed 
    // on screen the next time the app is started (only in debug configuration)
    /// </summary>
    //[Conditional("DEBUG")]
    public static void DisplayCrashReport()
    {
      const string errorFilename = "Fatal.log";
      var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
      var errorFilePath = Path.Combine(libraryPath, errorFilename);

      if (!File.Exists(errorFilePath))
      {
        return;
      }

      var errorText = File.ReadAllText(errorFilePath);
      var alertView = new UIAlertView("Crash Report", errorText, null, "Close", "Clear") { UserInteractionEnabled = true };
      alertView.Clicked += (sender, args) =>
      {
        if (args.ButtonIndex != 0)
        {
          File.Delete(errorFilePath);
        }
      };
      alertView.Show();
    }
  }
}