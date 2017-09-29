using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using TodoApp.BL.Extensions;
using TodoApp.BL.Services;
using TodoApp.Data.Models;
using TodoApp.Data.Services.Interfaces;

namespace TodoApp.BL.ViewModel.Pages
{
  public class EditItemPageViewModel : BindableBase, INavigatingAware, INavigatedAware
  {

    #region private fiels
    private readonly INavigationService _navigationService;
    private readonly ITodoItemStorageService _storageService;
    private readonly ITextToSpeechService _textToSpeachService;
    #endregion

    #region ctor
    public EditItemPageViewModel(INavigationService navigationService
                                 , ITodoItemStorageService storageService
                                 , ITextToSpeechService textToSpeachService)
    {
      _navigationService = navigationService;
      _storageService = storageService;
      _textToSpeachService = textToSpeachService;
    }
    #endregion

    private TodoItemViewModel _item;

    public TodoItemViewModel Item
    {
      get { return _item; }
      set { SetProperty(ref _item, value); }
    }

    private TodoItemViewModel _sourceViewModel;
    public void OnNavigatingTo(NavigationParameters parameters)
    {
      TodoItemViewModel vm;
      if (parameters.TryGetTypedParameter(out _sourceViewModel))
      {
        vm = new TodoItemViewModel(_sourceViewModel.Model.Clone());
      }
      else
      {
        vm = new TodoItemViewModel();
      }
      Item = vm;
    }

    private DelegateCommand _saveCommand;
    public DelegateCommand SaveCommand
    {
      get
      {
        return _saveCommand ?? (_saveCommand = new DelegateCommand(() =>
        {
          _storageService.SaveItemAsync(Item.Model).OnSuccess(() =>
          {
            var model = Item.Model;
            var par = new NavigationParameters();
            if (_sourceViewModel != null) //just update existing view model
            {
              _sourceViewModel.Model = model;
            }
            else //send a new created model
            {
              par.SetTypedParameter(Item);
            }
            _navigationService.GoBackAsync(par);
          });
        }));
      }
    }

    private DelegateCommand _cancelCommand;

    public DelegateCommand CancelCommand
    {
      get { return _cancelCommand ?? (_cancelCommand = new DelegateCommand(() => _navigationService.GoBackAsync())); }
    }

    private DelegateCommand _speakCommand;
    public DelegateCommand SpeakCommand
    {
      get { return _speakCommand ?? (_speakCommand = new DelegateCommand(() => _textToSpeachService.Say(Item.Name))); }
    }

    public void OnNavigatedFrom(NavigationParameters parameters)
    {
      throw new System.NotImplementedException();
    }

    public void OnNavigatedTo(NavigationParameters parameters)
    {
      throw new System.NotImplementedException();
    }
  }
}