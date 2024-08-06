using System.Collections.Generic;
using TodoList.Api.Models;
using TodoList.Api.Repositories;
using System.Threading.Tasks;

namespace TodoList.Api.Services;

public class TodoItemsService : ITodoItemsService
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public TodoItemsService(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }

    public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
    {
        return await _todoItemsRepository.CreateTodoItemAsync(todoItem);
    }
}
