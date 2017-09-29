using System;
using Microsoft.Practices.Unity;
using SQLite;
using TodoApp.BL.Navigation;
using TodoApp.BL.ViewModel.Pages;
using TodoApp.Data.Services;
using TodoApp.Data.Services.Interfaces;
using TodoApp.UI.Extensions;
using TodoApp.UI.View.Pages;
using Xamarin.Forms;

namespace TodoApp.UI
{
  public static class Startup
  {
    public static void ConfigureNavigation(IUnityContainer container)
    {
      Uri navpage;
      var root = new Uri("http://todo.app");

      container
        .RegisterRoute<NavigationPage>(out navpage)
        .RegisterRoute<TodoListPage, TodoListPageViewModel>(out NavigationTargets.TodoList)
        .RegisterRoute<EditItemPage, EditItemPageViewModel>(out NavigationTargets.EditItem);

      NavigationTargets.Home = new Uri(root, navpage.Combine(NavigationTargets.TodoList));
    }

    public static void ConfigureServices(IUnityContainer container)
    {
      container.RegisterType<ITodoItemStorageService, TodoItemStorageService>();
    }
  }
}