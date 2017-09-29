using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Android.Support.V7.App;
using TodoApp.BL.Reports;
using Xamarin.Forms.Platform.Android;

namespace TodoApp.Droid.Utils
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
      
#if DEBUG
      LogUnhandledException(newExc);
#endif
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
        var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
        DateTime.Now, exception.ToString());
        File.WriteAllText(errorFilePath, errorMessage);

        // Log to Android Device Logging.
        Android.Util.Log.Error("Crash Report", errorMessage);
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
    public static void DisplayCrashReport(FormsAppCompatActivity app)
    {
      const string errorFilename = "Fatal.log";
      var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
      var errorFilePath = Path.Combine(libraryPath, errorFilename);

      if (!File.Exists(errorFilePath))
      {
        return;
      }

      var errorText = File.ReadAllText(errorFilePath);
      new AlertDialog.Builder(app)
          .SetPositiveButton("Clear", (sender, args) =>
          {
            File.Delete(errorFilePath);
          })
          .SetNegativeButton("Close", (sender, args) =>
          {
                  // User pressed Close.
                })
          .SetMessage(errorText)
          .SetTitle("Crash Report")
          .Show();
    }
  }
}