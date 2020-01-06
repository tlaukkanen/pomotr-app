using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PomotrApp.Models;
using PomotrApp.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PomotrApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private ApplicationDbContext context;

        public FamiliesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Family
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamilies()
        {
            return await context.Families.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetFamily(Guid id)
        {
            var family = await context.Families.FindAsync(id);
            if(family==null) 
            {
                return NotFound();
            }
            return family;
        }

        [HttpPost]
        public async Task<IActionResult> PostFamily(Family family) 
        {
            context.Families.Update(family);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Family>> DeleteFamily(Guid id)
        {
            var family = await context.Families.FindAsync(id);
            if(family==null) 
            {
                return NotFound();
            }
            context.Families.Remove(family);
            await context.SaveChangesAsync();
            return family;
        }        
        
    }
    
}