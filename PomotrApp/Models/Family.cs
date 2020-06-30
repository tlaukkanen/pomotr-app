using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PomotrApp.Models
{
    public class Family
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Name { get; set; }

        public List<FamilyMember> Members { get; set; } = new List<FamilyMember>();
    }
    
}