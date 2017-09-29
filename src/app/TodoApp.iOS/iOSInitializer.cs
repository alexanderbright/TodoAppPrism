using System;
using System.IO;
using Microsoft.Practices.Unity;
using Prism.Unity;
using SQLite;
using TodoApp.BL.Services;
using TodoApp.iOS.Services;

namespace TodoApp.iOS
{
  public class iOSInitializer : IPlatformInitializer
  {
    private const string DatabaseFileName = "todo_database.db3";
    public void RegisterTypes(IUnityContainer container)
    {
      container.RegisterType<ITextToSpeechService, TextToSpeechService>();
      container.RegisterInstance(GetConnectionString());
    }
    private static SQLiteConnectionString GetConnectionString()
    {
      return new SQLiteConnectionString(Path.Combine(GetPersonalFolder(), DatabaseFileName), true);
    }

    private static string GetPersonalFolder()
    {
      string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

      if (!Directory.Exists(libFolder))
      {
        Directory.CreateDirectory(libFolder);
      }
      return libFolder;
    }
  }
}