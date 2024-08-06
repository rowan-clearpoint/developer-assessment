using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Moq;
using Xunit;
using TodoList.Api.Models;
using TodoList.Api.Repositories;
using TodoList.Api.Validations;
using System.Linq;

public class TodoItemTests
{
    [Fact]
    public void TodoItem_ShouldRequireDescription()
    {
        // Arrange
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            IsCompleted = false
        };

        // Act
        var validationResults = ValidateModel(todoItem);

        // Assert
        Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(TodoItem.Description)) && v.ErrorMessage.Contains("The Description field is required."));
    }

    [Fact]
    public void TodoItem_Description_ShouldHaveMinLength()
    {
        // Arrange
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = " ",
            IsCompleted = false
        };

        // Act
        var validationResults = ValidateModel(todoItem);

        // Assert
        Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The Description field is required.") || v.ErrorMessage.Contains("The field Description must be a string or array type with a minimum length of '1'."));
    }

    [Fact]
    public void TodoItem_Description_ShouldBeUnique()
    {
        // Arrange
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = "Existing Description",
            IsCompleted = false
        };

        var mockRepo = new Mock<ITodoItemsRepository>();
        mockRepo.Setup(repo => repo.GetTodoItemByDescriptionAsync(It.IsAny<string>()))
                .ReturnsAsync(new TodoItem { Description = "Existing Description" });

        var services = new ServiceCollection();
        services.AddSingleton(mockRepo.Object);
        var serviceProvider = services.BuildServiceProvider();

        var validationContext = new ValidationContext(todoItem, serviceProvider, null);

        // Act
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(todoItem, validationContext, validationResults, true);

        // Assert
        Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Description already exists."));
    }

    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
        Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);
        return validationResults;
    }
}
