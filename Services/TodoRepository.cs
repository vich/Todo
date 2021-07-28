﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.DbContexts;
using TodoApi.Entities;

namespace TodoApi.Services
{
    public class TodoRepository : ITodoRepository, IDisposable
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> TodoExists(Guid guid)
        {
            return await _context.TodoItems.AnyAsync(t => t.Id == guid);
        }

        public async Task<TodoItem> GeTodoItem(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(guid));
            }

            return await _context.TodoItems.FindAsync(guid);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(string city, string assignedTo)
        {
            var collection = _context.TodoItems as IQueryable<TodoItem>;

            if (!string.IsNullOrWhiteSpace(city))
            {
                city = city.Trim();
                collection = collection
                    .Where(x => x.City == city);
            }
            
            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                assignedTo = assignedTo.Trim();
                collection = collection
                    .Where(x => x.AssignTo == assignedTo);
            }

            return await collection.ToListAsync();
        }

        public async Task AddTodo(TodoItem todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            todo.Id = new Guid();

            await _context.TodoItems.AddAsync(todo);
        }

        public async Task UpdateTodo(TodoItem todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            var storedItem = await GeTodoItem(todo.Id);

            storedItem.Name = todo.Name;
            storedItem.DueDate = todo.DueDate;
            storedItem.Priority = todo.Priority;
            storedItem.Status = todo.Status;
            storedItem.City = todo.City;
            storedItem.AssignTo = todo.AssignTo;
        }

        public void DeleteTodo(TodoItem todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            _context.TodoItems.Remove(todo);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}