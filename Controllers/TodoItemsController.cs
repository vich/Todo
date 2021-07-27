using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoItemsController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems(
            [FromQuery(Name = "City")] string city, [FromQuery(Name = "AssignedTo")] string assignedTo)
        {
            var collection = await _todoRepository.GetTodoItems(city, assignedTo);

            return collection.Select(ItemToDTO).ToList();
        }

        // GET: api/TodoItems/id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(Guid id)
        {
            if (!await _todoRepository.TodoExists(id))
            {
                return NotFound();
            }

            var todoItem = await _todoRepository.GeTodoItem(id);
            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            
            if (!await _todoRepository.TodoExists(id))
            {
                return NotFound();
            }

            var todoItem = await _todoRepository.GeTodoItem(id);
            await _todoRepository.UpdateTodo(todoItem);

            try
            {
                await _todoRepository.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_todoRepository.TodoExists(id).Result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Id = new Guid(),
                Name = todoItemDTO.Name,
                DueDate = todoItemDTO.DueDate,
                Priority = todoItemDTO.Priority,
                Status = todoItemDTO.Status,
                City = todoItemDTO.City,
                AssignTo = todoItemDTO.AssignTo,
            };

            await _todoRepository.AddTodo(todoItem);
            await _todoRepository.Save();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new {id = todoItem.Id},
                ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            if (!await _todoRepository.TodoExists(id))
            {
                return NotFound();
            }

            var todoItem = await _todoRepository.GeTodoItem(id);
            _todoRepository.DeleteTodo(todoItem);
            await _todoRepository.Save();

            return NoContent();
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                DueDate = todoItem.DueDate,
                Priority = todoItem.Priority,
                Status = todoItem.Status,
                City = todoItem.City,
                AssignTo = todoItem.AssignTo,
            };
    }
}