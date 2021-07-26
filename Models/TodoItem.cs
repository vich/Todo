using System;
using System.ComponentModel.DataAnnotations;
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
        
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string AssignTo { get; set; }
        
        public string Secret { get; set; }
    }
}