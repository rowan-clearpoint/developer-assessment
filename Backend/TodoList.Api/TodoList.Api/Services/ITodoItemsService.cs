using System.Collections.Generic;
using TodoList.Api.Models;
using System.Threading.Tasks;

namespace TodoList.Api.Services;

public interface ITodoItemsService
{
    Task<TodoItem> CreateTodoItemAsync(TodoItem item);
}