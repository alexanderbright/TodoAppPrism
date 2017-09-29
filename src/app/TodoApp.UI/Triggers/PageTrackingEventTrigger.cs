using System.Collections.Generic;
using TodoApp.BL.Reports;
using Xamarin.Forms;

namespace TodoApp.UI.Triggers
{
  public class PageTrackingEventTrigger : TriggerAction<Page>
  {
    protected override void Invoke(Page page)
    {
      AppReports.TrackPage(page.GetType().Name, new KeyValuePair<string, string>(AppReports.PageTitleKey, page.Title ?? ""));
    }
  }
}