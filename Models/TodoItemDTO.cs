using System;
using TodoApi.Enums;

namespace TodoApi.Models
{
    public class TodoItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string City { get; set; }
        public string AssignTo { get; set; }
    }
}