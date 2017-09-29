using System;
using System.Runtime.CompilerServices;
using Prism.Mvvm;

namespace TodoApp.BL.ViewModel
{
  public class ViewModelBase<T> : BindableBase where T : class, new()
  {
    private bool _isModelSet;

    public bool IsModelSet
    {
      get { return _isModelSet; }
      set
      {
        if (SetProperty(ref _isModelSet, value))
          RaisePropertyChanged(nameof(IsModelEmpty));
      }
    }

    public bool IsModelEmpty
    {
      get { return !_isModelSet; }
    }

    protected T _model;

    public T Model
    {
      get { return _model; }
      set
      {
        if (ReferenceEquals(_model, value))
          return;
        if (value == null)
        {
          IsModelSet = false;
          _model = new T();
        }
        else
        {
          IsModelSet = true;
          _model = value; //model changed
        }
        RaisePropertyChanged(string.Empty); //view model changed
      }
    }

    public void ResetModel()
    {
      Model = null;
    }

    public ViewModelBase(T model)
    {
      _isModelSet = model != null;
      _model = _isModelSet ? model : new T();
    }

    public ViewModelBase() : this(default(T))
    {
    }

    protected virtual bool SetProperty<TProperty>(TProperty current, Action<TProperty> setter, TProperty value, [CallerMemberName] string propertyName = null)
    {
      if (!SetProperty(ref current, value, propertyName))
        return false;

      setter(value);
      return true;
    }
  }
}