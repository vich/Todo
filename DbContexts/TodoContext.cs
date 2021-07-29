using System;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Entities;
using Todo.Api.Enums;

namespace Todo.Api.DbContexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = Guid.Parse("36a12bfa-d2c4-4dc9-9906-eae63b4319a0"),
                    City = "city1",
                    AssignTo = "a1@gmail.com",
                    DueDate = DateTime.Now,
                    Name = "Todo 1",
                    Priority = Priority.Low,
                    Status = Status.InProgress
                },
                new TodoItem
                {
                    Id = Guid.Parse("36a12bfa-1111-2222-3333-eae63b4319a0"),
                    City = "city2",
                    AssignTo = "a2@gmail.com",
                    DueDate = DateTime.Now,
                    Name = "Todo 2",
                    Priority = Priority.Critical,
                    Status = Status.ToDo
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}