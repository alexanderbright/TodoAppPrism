using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TodoApp.UI.Tests
{
  [TestFixture(Platform.Android)]
  //[TestFixture(Platform.iOS)]
  public class Tests
  {
    private IApp _app;
    private readonly Platform _platform;
    private static readonly Func<AppQuery, AppQuery> AddButton = c => c.Marked("toolbar_add");

    public Tests(Platform platform)
    {
      this._platform = platform;
    }

    [SetUp]
    public void BeforeEachTest()
    {
      _app = AppInitializer.StartApp(_platform);
    }

    [Test]
    public void AppLaunches()
    {
      _app.Screenshot("First screen.");
    }

    [Test]
    public void AddItemTest()
    {
      //_app.WaitForElement(AddButton);
      _app.Tap(AddButton);
    }
  }
}

