using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TodoApp.UI.Tests
{
  public class AppInitializer
  {
    public static IApp StartApp(Platform platform)
    {
      if (platform == Platform.Android)
      {
        return ConfigureApp
          .Android
          .Debug()
          .DeviceSerial("4d006075c49e81cd")
          .ApkFile(@"C:\Users\o.biloborodov\AppData\Local\Xamarin\Mono for Android\Archives\TodoApp.Android.apk")
          .StartApp();
      }

      return ConfigureApp
        .iOS
        .StartApp();
    }
  }
}

