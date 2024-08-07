using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.Api.Controllers;
using TodoList.Api.Models;
using TodoList.Api.Services;
using Xunit;

namespace TodoList.Api.UnitTests.Controllers
{
    public class TodoItemsControllerTests
    {
        private readonly Mock<ITodoItemsService> _mockTodoItemsService;
        private readonly TodoItemsController _controller;

        public TodoItemsControllerTests()
        {
            _mockTodoItemsService = new Mock<ITodoItemsService>();
            _controller = new TodoItemsController(null, _mockTodoItemsService.Object);
        }

        [Fact]
        public async Task PostTodoItem_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Description = "Sample Todo",
                IsCompleted = false
            };

            _mockTodoItemsService
                .Setup(service => service.CreateTodoItemAsync(It.IsAny<TodoItem>()))
                .ReturnsAsync(todoItem);

            // Act
            var result = await _controller.PostTodoItem(todoItem);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedTodoItem = Assert.IsType<TodoItem>(actionResult.Value);

            Assert.Equal(todoItem.Id, actionResult.RouteValues["id"]);
            Assert.Equal(todoItem, returnedTodoItem);
        }
    }
}
