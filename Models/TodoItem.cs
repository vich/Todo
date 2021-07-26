using System;
using TodoApi.Enums;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string City { get; set; }
        public string AssignTo { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}