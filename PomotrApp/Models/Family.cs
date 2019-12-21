using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PomotrApp.Models
{
    public class Family
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public List<FamilyMember> Members { get; set; }
    }
    
}