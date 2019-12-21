using System;
using System.ComponentModel.DataAnnotations;

namespace PomotrApp.Models
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }        
        public Errand Errand { get; set; }
        public int Points { get; set; }
        public int MyProperty { get; set; }
        public DateTime DateCompleted { get; set; }
    }
    
}