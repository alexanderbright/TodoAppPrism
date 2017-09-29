using Xamarin.Forms;

namespace TodoApp.UI.Triggers
{
  public class ClearSelectionEventTrigger : TriggerAction<ListView>
  {
    protected override void Invoke(ListView sender)
    {
      sender.SelectedItem = null;
    }
  }
}