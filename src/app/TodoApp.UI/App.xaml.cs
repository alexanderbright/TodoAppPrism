using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Unity;
using TodoApp.BL.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TodoApp.UI
{
  public partial class App : PrismApplication
  {
    public App():this(null)
    {
    }

    public App(IPlatformInitializer initializer) : base(initializer) { }
    protected override void OnInitialized()
    {
      NavigationService.NavigateAsync(NavigationTargets.Home);
    }

    protected override void RegisterTypes()
    {
      Startup.ConfigureNavigation(Container);
      Startup.ConfigureServices(Container);
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps      
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}