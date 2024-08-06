using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Moq;
using Xunit;
using TodoList.Api.Models;
using TodoList.Api.Repositories;
using TodoList.Api;

public class TodoItemsRepositoryTests
{
    [Fact]
    public async Task CreateTodoItemAsync_ShouldAddNewItem()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        await using var context = new TodoContext(options);
        var repository = new TodoItemsRepository(context);

        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = "Test Item",
            IsCompleted = false
        };

        // Act
        var result = await repository.CreateTodoItemAsync(todoItem);

        // Assert
        var createdItem = await context.TodoItems.FindAsync(result.Id);
        Assert.NotNull(createdItem);
        Assert.Equal("Test Item", createdItem.Description);
    }

    [Fact]
    public async Task GetTodoItemByDescriptionAsync_ShouldReturnCorrectItem()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        await using var context = new TodoContext(options);
        var repository = new TodoItemsRepository(context);

        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = "Unique Description",
            IsCompleted = false
        };

        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetTodoItemByDescriptionAsync("Unique Description");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(todoItem.Id, result.Id);
    }

    [Fact]
    public async Task GetTodoItemByDescriptionAsync_ShouldReturnNullIfNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        await using var context = new TodoContext(options);
        var repository = new TodoItemsRepository(context);

        // Act
        var result = await repository.GetTodoItemByDescriptionAsync("Non-Existent Description");

        // Assert
        Assert.Null(result);
    }
}
