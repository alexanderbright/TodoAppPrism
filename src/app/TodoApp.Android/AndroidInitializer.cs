using System;
using System.IO;
using Microsoft.Practices.Unity;
using Prism.Unity;
using SQLite;
using TodoApp.BL.Reports;
using TodoApp.BL.Services;
using TodoApp.Droid.Services;

namespace TodoApp.Droid
{
  public class AndroidInitializer : IPlatformInitializer
  {
    private const string DatabaseFileName = "todo_database.db3";
    public void RegisterTypes(IUnityContainer container)
    {
      container.RegisterType<ITextToSpeechService, TextToSpeechService>();
      container.RegisterInstance(GetConnectionString());
    }

    private static SQLiteConnectionString GetConnectionString()
    {
      var dbPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseFileName);
      return new SQLiteConnectionString(dbPath, true);
    }
  }
}