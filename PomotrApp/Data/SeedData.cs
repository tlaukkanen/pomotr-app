using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using PomotrApp.Models;

namespace PomotrApp.Data
{
    public class SeedData
    {
        private ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Generate()
        {
            if(!_context.Users.Any())
            {
                SeedUsers();
            }
        }

        private void SeedUsers()
        {
            string email = "admin@admin.com";
            var defaultAdmin = new ApplicationUser
            {
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                PhoneNumber = "",
                PhoneNumberConfirmed = false,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            _context.Users.Add(defaultAdmin);
            _context.SaveChanges();

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashedPwd = passwordHasher.HashPassword(defaultAdmin, "DefaultPassword");
            defaultAdmin.PasswordHash = hashedPwd;
        }


    }
}