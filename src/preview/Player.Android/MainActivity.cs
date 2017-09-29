using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using UXDivers.Gorilla;

namespace Player.Droid
{
	[Activity (Label = "Player", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(this, new Config("Gorilla on LAPTOP")
        .RegisterAssembliesFromTypes<TodoApp.BL.Services.ITextToSpeechService
                                        , TodoApp.UI.App
                                        , TodoApp.Data.Services.TodoItemStorageService
                                        , Prism.Unity.PrismApplication
                                        , Prism.Common.ApplicationProvider
                                        , Prism.IActiveAware
                                        , Xamarin.Forms.Animation
                                        , Xamarin.Forms.Xaml.ArrayExtension>()));
		}

	  public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
	  {
	    if ((keyCode == Keycode.Menu || keyCode == Keycode.F1) && Coordinator.Instance != null)
	    {
	      Coordinator.Instance.RequestStatusUpdate();
	      return true;
	    }

	    return base.OnKeyUp(keyCode, e);
	  }

	  protected override void OnDestroy()
	  {
	    {
	      try
	      {
	        base.OnDestroy();
	      }
	      catch (Exception ex)
	      {
	        ex.ToString();
	      }
	    }
	  }
  }
}

