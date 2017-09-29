using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using TodoApp.BL.Extensions;
using TodoApp.BL.Navigation;
using TodoApp.Data.Models;
using TodoApp.Data.Services.Interfaces;

namespace TodoApp.BL.ViewModel.Pages
{
  public class TodoListPageViewModel : BindableBase, INavigatingAware
  {
    private readonly INavigationService _navigationService;
    private readonly ITodoItemStorageService _todoItemStorageService;

    public TodoListPageViewModel(INavigationService navigationService
                                 , ITodoItemStorageService todoItemStorageService)
    {
      _navigationService = navigationService;
      _todoItemStorageService = todoItemStorageService;

      todoItemStorageService.GetItemsAsync()
        .OnSuccess((list) =>
          Items = new ObservableCollection<TodoItemViewModel>(list.Select(i => new TodoItemViewModel(i))));
    }

    private ObservableCollection<TodoItemViewModel> _items;

    public ObservableCollection<TodoItemViewModel> Items
    {
      get { return _items; }
      set { SetProperty(ref _items, value); }
    }

    private DelegateCommand _addItemCommand;
    public DelegateCommand AddItemCommand
    {
      get
      {
        return _addItemCommand ?? (_addItemCommand =
                 new DelegateCommand(() => _navigationService.NavigateAsync(NavigationTargets.EditItem)));
      }
    }

    private DelegateCommand<TodoItemViewModel> _editItemCommand;
    public DelegateCommand<TodoItemViewModel> EditItemCommand { get
      {

        var nav = new NavigationParameters();
        nav.Add("item", null);

        return _editItemCommand ?? (_editItemCommand = new DelegateCommand<TodoItemViewModel>((item) =>
                 _navigationService.NavigateAsync(NavigationTargets.EditItem
                                                  , new NavigationParameters().SetTypedParameter(item))));
      } }

    public void OnNavigatingTo(NavigationParameters parameters)
    {
      if (parameters.GetNavigationMode() == NavigationMode.Back)
      {
        TodoItemViewModel itemViewModel;
        if (parameters.TryGetTypedParameter(out itemViewModel))
        {
          _items.Add(itemViewModel);
        }
      }
    }
  }
}