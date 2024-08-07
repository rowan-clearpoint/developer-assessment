﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Models;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITodoItemsService _todoItemsService;

        public TodoItemsController(TodoContext context, ITodoItemsService todoItemsService)
        {
            _context = context;
            _todoItemsService = todoItemsService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var results = await _context.TodoItems.Where(x => !x.IsCompleted).ToListAsync();
            return Ok(results);
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var result = await _context.TodoItems.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemIdExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            await _todoItemsService.CreateTodoItemAsync(todoItem);
             
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        } 

        private bool TodoItemIdExists(Guid id)
        {
            return _context.TodoItems.Any(x => x.Id == id);
        }
    }
}
