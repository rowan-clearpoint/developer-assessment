using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Models;
using TodoList.Api.Repositories;

namespace TodoList.Api.Validations;

public class TodoItemUniqueDescriptionAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var description = value.ToString();
        var todoItemsRepository = (ITodoItemsRepository)validationContext.GetService(typeof(ITodoItemsRepository));

        if (todoItemsRepository == null)
        {
            throw new InvalidOperationException("TodoItemsRepository not found.");
        }

        var exists = todoItemsRepository.GetTodoItemByDescriptionAsync(description).GetAwaiter().GetResult();

        if (exists != null)
        {
            return new ValidationResult("Description already exists.");
        }

        return ValidationResult.Success;
    }
}
