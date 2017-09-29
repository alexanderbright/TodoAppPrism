using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using TodoApp.BL.Reports;
using TodoApp.iOS.Services;
using TodoApp.iOS.Utils;
using TodoApp.UI;
using UIKit;

namespace TodoApp.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
		  CrashReporter.Init();
      AppReports.Reports = new GoogleAnalyticsService(AppReports.TrackingId);

      global::Xamarin.Forms.Forms.Init ();
		  CrashReporter.DisplayCrashReport();

      LoadApplication (new App(new iOSInitializer()));

			return base.FinishedLaunching (app, options);
		}
	}
}
