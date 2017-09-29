using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoApp.UI.View.Pages
{
  public partial class HomeNavigationPage : NavigationPage, INavigationPageOptions
  {
    public HomeNavigationPage()
    {
      InitializeComponent();
    }

    public bool ClearNavigationStackOnNavigation => true;
  }
}