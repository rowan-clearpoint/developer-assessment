using System.Collections.Generic;
using TodoList.Api.Models;
using System.Threading.Tasks;

namespace TodoList.Api.Repositories
{
    public interface ITodoItemsRepository
    {
        Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);
        Task<TodoItem> GetTodoItemByDescriptionAsync(string description);
    }
}