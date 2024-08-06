using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using TodoList.Api.Models;
using TodoList.Api.Repositories;
using TodoList.Api.Services;

public class TodoItemsServiceTests
{
    [Fact]
    public async Task CreateTodoItemAsync_ShouldCallRepositoryAndReturnCreatedItem()
    {
        // Arrange
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = "Test Item",
            IsCompleted = false
        };

        var mockRepo = new Mock<ITodoItemsRepository>();
        mockRepo.Setup(repo => repo.CreateTodoItemAsync(It.IsAny<TodoItem>()))
                .ReturnsAsync(todoItem);

        var service = new TodoItemsService(mockRepo.Object);

        // Act
        var result = await service.CreateTodoItemAsync(todoItem);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(todoItem.Id, result.Id);
        Assert.Equal(todoItem.Description, result.Description);
        Assert.Equal(todoItem.IsCompleted, result.IsCompleted);

        mockRepo.Verify(repo => repo.CreateTodoItemAsync(It.IsAny<TodoItem>()), Times.Once);
    }
}
