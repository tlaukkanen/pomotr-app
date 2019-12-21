using System;
using System.ComponentModel.DataAnnotations;

namespace PomotrApp.Models
{
    public class FamilyMember
    {
        public enum Role
        {
            Parent,
            Child
        }

        [Key]
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }        
    }
    
}