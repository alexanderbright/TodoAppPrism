using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Prism.Navigation;
using TodoApp.BL.Extensions;
using TodoApp.BL.Services;
using TodoApp.BL.ViewModel;
using TodoApp.BL.ViewModel.Pages;
using TodoApp.Data.Models;
using TodoApp.Data.Services.Interfaces;

namespace TodoApp.BL.Tests.ViewModel.Pages
{
  [TestFixture]
  public class EditItemPageViewModelTests
  {
    private EditItemPageViewModel _viewModel;
    private Mock<INavigationService> _fakeNavigationService;
    private Mock<ITodoItemStorageService> _fakeStorageService;
    private Mock<ITextToSpeechService> _fakeTextToSpeechService;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
    }

    [SetUp]
    public void Setup()
    {
      _fakeNavigationService = new Mock<INavigationService>();
      _fakeStorageService = new Mock<ITodoItemStorageService>();
      _fakeTextToSpeechService = new Mock<ITextToSpeechService>();
      _viewModel = new EditItemPageViewModel(_fakeNavigationService.Object
                                              , _fakeStorageService.Object
                                              , _fakeTextToSpeechService.Object);
    }

    [Test]
    public void NavigationEditExistingTest()
    {
      TodoItemViewModel vm = CreateViewModel();
      _viewModel.OnNavigatingTo(new NavigationParameters().SetTypedParameter(vm));
      Assert.That(_viewModel.Item.Model, Is.EqualTo(vm.Model), "Models are not equivalent");
      Assert.That(ReferenceEquals(_viewModel.Item.Model, vm.Model), Is.False, "Model is not cloned");
    }

    [Test]
    public void NavigationCreateNewTest()
    {
      TodoItemViewModel vm = CreateViewModel();
      _viewModel.OnNavigatingTo(new NavigationParameters().SetTypedParameter(vm));
      Assert.That(_viewModel.Item, Is.Not.Null, "Item view model is not set");
      Assert.That(_viewModel.Item.Model, Is.Not.Null, "Model is not set");
    }

    [Test]
    public void SayCommandTest()
    {
      TodoItemViewModel vm = CreateViewModel();
      _viewModel.OnNavigatingTo(new NavigationParameters().SetTypedParameter(vm));

      _viewModel.SpeakCommand.Execute();
      _fakeTextToSpeechService.Verify(s => s.Say(vm.Name), Times.Once);
    }

    [Test]
    public async Task SaveCommandExisitngItemTest()
    {
      TodoItemViewModel vm = CreateViewModel();
      _viewModel.OnNavigatingTo(new NavigationParameters().SetTypedParameter(vm)); //set model to edit
      var result = Task.FromResult(1);
      _fakeStorageService.Setup(s => s.SaveItemAsync(It.IsAny<TodoItem>())).Returns(result);

      _viewModel.SaveCommand.Execute();
      _fakeStorageService.Verify(s => s.SaveItemAsync(_viewModel.Item.Model), Times.Once);
      await result;
      _fakeNavigationService.Verify(s => s.GoBackAsync(It.Is<NavigationParameters>(p => !p.TryGetTypedParameter(out vm)), null, true), Times.Once);
    }

    [Test]
    public async Task SaveCommandNewItemTest()
    {
      _viewModel.OnNavigatingTo(new NavigationParameters()); //no model to edit
      var result = Task.FromResult(1);
      _fakeStorageService.Setup(s => s.SaveItemAsync(It.IsAny<TodoItem>())).Returns(result);

      _viewModel.SaveCommand.Execute();
      _fakeStorageService.Verify(s => s.SaveItemAsync(_viewModel.Item.Model), Times.Once);
      await result;
      TodoItemViewModel vm;
      _fakeNavigationService.Verify(s => s.GoBackAsync(It.Is<NavigationParameters>(p => p.TryGetTypedParameter(out vm)), null, true), Times.Once);
    }

    [Test]
    public void CancelCommandTest()
    {
      TodoItemViewModel vm = CreateViewModel();
      _viewModel.OnNavigatingTo(new NavigationParameters().SetTypedParameter(vm));

      _viewModel.CancelCommand.Execute();
      _fakeNavigationService.Verify(s => s.GoBackAsync(null, null, true), Times.Once);
    }

    private static TodoItemViewModel CreateViewModel()
    {
      return new TodoItemViewModel(new TodoItem()
      {
        Id = 1,
        Done = false,
        Name = "Fake item",
        Notes = "Fake notes"
      });
    }
  }
}