using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Services;
using Xunit;
using Todo.Api.DbContexts;
using Todo.Api.Entities;
using Todo.Api.Enums;

namespace Todo.Api.Test
{
    public class TodoRepositoryTest
    {
        #region Members

        private TodoContext _context;
        private readonly TodoItem _todo1 = new TodoItem
        {
            Id = Guid.Parse("36a12bfa-d2c4-4dc9-9906-eae63b4319a0"),
            City = "city1",
            AssignTo = "a1@gmail.com",
            DueDate = DateTime.Now,
            Name = "Todo 1",
            Priority = Priority.Low,
            Status = Status.InProgress
        };
        private readonly TodoItem _todo2 = new TodoItem
        {
            Id = Guid.Parse("36a12bfa-1111-2222-3333-eae63b4319a0"),
            City = "city2",
            AssignTo = "a2@gmail.com",
            DueDate = DateTime.Now,
            Name = "Todo 2",
            Priority = Priority.Critical,
            Status = Status.ToDo
        };

        #endregion Members

        #region Tests

        [Theory]
        [InlineData("36a12bfa-d2c4-4dc9-9906-eae63b4319a0", true)]
        [InlineData("36a12bfa-1111-2222-3333-eae63b4319a0", true)]
        [InlineData("36a12bfa-9999-2222-9999-eae63b4319a0", false)]
        public async Task TodoExistsTest(string guid, bool exists)
        {
            CreateCleanContext($"{nameof(TodoExistsTest)}-{guid}");

            var todoRepository = new TodoRepository(_context);

            var existsItem = await todoRepository.TodoExists(Guid.Parse(guid));

            Assert.Equal(exists, existsItem);
        }

        [Fact]
        public async Task GeTodoItemTest()
        {
            CreateCleanContext($"{nameof(GeTodoItemTest)}");

            var todoRepository = new TodoRepository(_context);
            var todoItem = await todoRepository.GeTodoItem(Guid.Parse("36a12bfa-d2c4-4dc9-9906-eae63b4319a0"));
            Assert.Equal(_todo1, todoItem);
        }

        [Theory]
        [InlineData("city1", null, 1)]
        [InlineData("city2", null, 1)]
        [InlineData("city2", "a1@gmail.com", 0)]
        [InlineData("city2", "a2@gmail.com", 1)]
        [InlineData(null, "a2@gmail.com", 1)]
        [InlineData(null, null, 2)]
        public async Task GetTodoItemsTest(string city, string assignedTo, int itemsCount)
        {
            CreateCleanContext($"{nameof(GetTodoItemsTest)}-{city}-{assignedTo}-{itemsCount}");

            var todoRepository = new TodoRepository(_context);
            var todoItems = await todoRepository.GetTodoItems(city, assignedTo);
            Assert.Equal(itemsCount, todoItems.Count());
        }


        [Theory]
        [InlineData("todo11", Priority.Low, Status.Done, "city1", "a1@gamil.com", false)]
        [InlineData("todo11", Priority.Low, Status.Done, "city1", null, true)] //Entity framework will throw exception after serialization 
        [InlineData("todo11", Priority.Low, Status.Done, null, "a1@gamil.com", true)] //Entity framework will throw exception after serialization
        [InlineData("todo11", Priority.Low, Status.Done, "city1", "a1", true)] //Entity framework will throw exception after serialization
        [InlineData(null, Priority.Low, Status.Done, "city1", "a1@gamil.com", true)] //Entity framework will throw exception after serialization
        public async Task AddTodoTest(string name, Priority priority, Status status, string city, string assignTo,
            bool shouldFail)
        {
            CreateCleanContext($"{nameof(AddTodoTest)}-{name}-{priority}-{status}-{city}-{assignTo}");

            var todoRepository = new TodoRepository(_context);

            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                City = city,
                AssignTo = assignTo,
                DueDate = DateTime.Now,
                Name = name,
                Priority = priority,
                Status = status
            };

            await todoRepository.AddTodo(todoItem);
        }

        [Fact]
        public async Task AddNullTodoTest()
        {
            CreateCleanContext($"{nameof(AddNullTodoTest)}");

            var todoRepository = new TodoRepository(_context);
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await todoRepository.AddTodo(null));
            Assert.Equal("Value cannot be null. (Parameter 'todo')", exception.Message);
        }

        [Fact]
        public async Task DeleteTodoTest()
        {
            CreateCleanContext($"{nameof(DeleteTodoTest)}");

            var todoRepository = new TodoRepository(_context);
            todoRepository.DeleteTodo(_todo1);
            var savedItems = await todoRepository.Save();
            var item = await todoRepository.GeTodoItem(_todo1.Id);
            Assert.Null(item);
            Assert.Equal(1, savedItems);
        }

        [Fact]
        public void DeleteNullTodoTest()
        {
            CreateCleanContext($"{nameof(DeleteNullTodoTest)}");

            var todoRepository = new TodoRepository(_context);
            
            var exception =
                Assert.Throws<ArgumentNullException>( () => todoRepository.DeleteTodo(null));
            Assert.Equal("Value cannot be null. (Parameter 'todo')", exception.Message);
        }

         [Fact]
        public void SaveTest()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: $"TodoItemsDatabase-{nameof(SaveTest)}")
                .Options;

            // Insert seed data into the database using one instance of the context
            _context = new TodoContext(options);
            _context.TodoItems.Add(_todo1);
            _context.TodoItems.Add(_todo2);
            var savedItems = _context.SaveChanges();

            Assert.Equal(2, savedItems);
        }

        #endregion Tests

        #region Private Methods

        private void CreateCleanContext(string name)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: $"TodoItemsDatabase-{name}")
                .Options;

            // Insert seed data into the database using one instance of the context
            _context = new TodoContext(options);
            _context.TodoItems.Add(_todo1);
            _context.TodoItems.Add(_todo2);
            _context.SaveChanges();
        }

        #endregion Private Methods
    }
}
