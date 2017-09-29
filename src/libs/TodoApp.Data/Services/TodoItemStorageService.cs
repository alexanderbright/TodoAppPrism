using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TodoApp.Data.Models;
using TodoApp.Data.Services.Interfaces;

namespace TodoApp.Data.Services
{
  public class TodoItemStorageService : ITodoItemStorageService
  {
    private readonly SQLiteAsyncConnection _connection;

    public TodoItemStorageService(SQLiteConnectionString connectionString)
    {
      _connection = new SQLiteAsyncConnection(connectionString.DatabasePath, connectionString.StoreDateTimeAsTicks);
      _connection.CreateTableAsync<TodoItem>().Wait();
    }

    public Task<List<TodoItem>> GetItemsAsync()
    {
      return _connection.Table<TodoItem>().ToListAsync();
    }

    public Task<List<TodoItem>> GetItemsNotDoneAsync()
    {
      return _connection.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }

    public Task<TodoItem> GetItemAsync(int id)
    {
      return _connection.FindAsync<TodoItem>(id);
    }

    public Task<int> SaveItemAsync(TodoItem item)
    {
      return item.Id != 0 ? _connection.UpdateAsync(item) : _connection.InsertAsync(item);
    }

    public Task<int> DeleteItemAsync(TodoItem item)
    {
      return _connection.DeleteAsync(item);
    }
  }
}