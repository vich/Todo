using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Entities;

namespace TodoApi.Services
{
    public interface ITodoRepository
    {
        Task<bool> TodoExists(Guid guid);
        Task<TodoItem> GeTodoItem(Guid guid);
        Task<IEnumerable<TodoItem>> GetTodoItems(string city, string assignedTo);
        Task AddTodo(TodoItem todo);
        Task UpdateTodo(TodoItem todo);
        void DeleteTodo(TodoItem todo);
        Task<int> Save();
    }
}