using System;
using System.ComponentModel.DataAnnotations;
using TodoList.Api.Validations;

namespace TodoList.Api.Models;

public class TodoItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MinLength(1)]
    [TodoItemUniqueDescriptionAttribute]
    public string Description { get; set; }

    public bool IsCompleted { get; set; }
}
