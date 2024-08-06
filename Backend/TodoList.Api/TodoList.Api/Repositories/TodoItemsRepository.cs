using System.Collections.Generic;
using System.Linq;
using TodoList.Api.Models;
using System.Threading.Tasks;

namespace TodoList.Api.Repositories
    {
    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly TodoContext _context;

        public TodoItemsRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> GetTodoItemByDescriptionAsync(string description)
        {
            return _context.TodoItems
            .SingleOrDefault(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
        }
    }
}