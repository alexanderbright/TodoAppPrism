using Prism.Mvvm;
using TodoApp.Data.Models;

namespace TodoApp.BL.ViewModel
{
  public class TodoItemViewModel : ViewModelBase<TodoItem>
  {
    public TodoItemViewModel(TodoItem model = null):base(model)
    {
    }


    public int Id => _model.Id;

    public string Name
    {
      get { return _model.Name; }
      set { SetProperty(_model.Name, (v) => _model.Name = v, value); }
    }

    public string Notes
    {
      get { return _model.Notes; }
      set { SetProperty(_model.Notes, (v) => _model.Notes = v, value ); }
    }

    public bool Done
    {
      get { return _model.Done; }
      set { SetProperty(_model.Done, (v) => _model.Done = v, value); }
    }
  }
}