using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TodoApp.BL.Reports;
using TodoApp.Droid.Services;
using TodoApp.Droid.Utils;
using TodoApp.UI;

namespace TodoApp.Droid
{
	[Activity (Label = "TodoApp", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
      CrashReporter.Init();
		  AppReports.Reports = new GoogleAnalyticsService(AppReports.TrackingId, ApplicationContext);

      TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
		  
      base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
      CrashReporter.DisplayCrashReport(this);

      LoadApplication (new App(new AndroidInitializer()));
		}
	}
}

