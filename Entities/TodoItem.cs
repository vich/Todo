using System;
using System.ComponentModel.DataAnnotations;
using TodoApi.Enums;

namespace TodoApi.Entities
{
    public class TodoItem
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public DateTime DueDate { get; set; }
        
        [Required]
        public Priority Priority { get; set; }
        
        [Required]
        public Status Status { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string AssignTo { get; set; }
    }
}