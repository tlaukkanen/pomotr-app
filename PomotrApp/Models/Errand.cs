using System;
using System.ComponentModel.DataAnnotations;

namespace PomotrApp.Models
{
    public class Errand
    {
    
        [Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public int RewardPoints { get; set; }
        
    }
    
}