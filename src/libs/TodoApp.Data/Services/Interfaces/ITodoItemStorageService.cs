using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Data.Models;

namespace TodoApp.Data.Services.Interfaces
{
  public interface ITodoItemStorageService
  {
    Task<List<TodoItem>> GetItemsAsync();
    Task<List<TodoItem>> GetItemsNotDoneAsync();
    Task<TodoItem> GetItemAsync(int id);
    Task<int> SaveItemAsync(TodoItem item);
    Task<int> DeleteItemAsync(TodoItem item);
  }
}