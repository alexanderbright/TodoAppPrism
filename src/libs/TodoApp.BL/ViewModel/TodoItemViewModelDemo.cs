using System;
using Prism.Mvvm;
using TodoApp.Data.Models;

namespace TodoApp.BL.ViewModel
{
  public class TodoItemViewModelDemo : BindableBase
  {
    private readonly TodoItemDemo _model;

    public TodoItemViewModelDemo(TodoItemDemo model)
    {
      if (model == null)
        throw new ArgumentNullException(nameof(model));
      _model = model;
    }

    public int Id
    {
      get { return _model.Id; }
    }

    public string Name
    {
      get { return _model.Name; }
      set { SetProperty(ref _model.Name, value); }
    }
  }
}