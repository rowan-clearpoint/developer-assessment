using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using TodoList.Api.Repositories;
using TodoList.Api.Models;
using TodoList.Api.Validations;
public class TodoItemUniqueDescriptionAttributeTests
{
    private readonly Mock<ITodoItemsRepository> _mockTodoItemsRepository;
    private readonly TodoItemUniqueDescriptionAttribute _attribute;

    public TodoItemUniqueDescriptionAttributeTests()
    {
        _mockTodoItemsRepository = new Mock<ITodoItemsRepository>();
        _attribute = new TodoItemUniqueDescriptionAttribute();
    }

    [Fact]
    public async Task IsValid_ExistingDescription_ReturnsValidationError()
    {
        // Arrange
        var description = "Existing Description";
        _mockTodoItemsRepository.Setup(repo => repo.GetTodoItemByDescriptionAsync(description))
            .ReturnsAsync(new TodoItem()); // Simulate existing item

        var serviceProvider = GetServiceProvider();
        var context = new ValidationContext(instance: new { Description = description }, serviceProvider: serviceProvider, items: null);

        // Act
        var result = await Task.Run(() => _attribute.GetValidationResult(description, context));

        // Assert
        Assert.Equal("Description already exists.", result.ErrorMessage);
    }

    [Fact]
    public async Task IsValid_NewDescription_ReturnsSuccess()
    {
        // Arrange
        var description = "New Description";
        _mockTodoItemsRepository.Setup(repo => repo.GetTodoItemByDescriptionAsync(description))
            .ReturnsAsync((TodoItem)null); // Simulate non-existing item

        var serviceProvider = GetServiceProvider();
        var context = new ValidationContext(instance: new { Description = description }, serviceProvider: serviceProvider, items: null);

        // Act
        var result = await Task.Run(() => _attribute.GetValidationResult(description, context));

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    private IServiceProvider GetServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(_mockTodoItemsRepository.Object);
        return serviceCollection.BuildServiceProvider();
    }
}
