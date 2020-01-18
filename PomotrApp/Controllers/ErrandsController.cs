using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PomotrApp.Models;
using PomotrApp.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PomotrApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrandsController : ControllerBase
    {
        private ApplicationDbContext context;

        public ErrandsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Errand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Errand>>> GetErrands()
        {
            return await context.Errands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Errand>> GetErrand(Guid id)
        {
            var Errand = await context.Errands.FindAsync(id);
            if(Errand==null) 
            {
                return NotFound();
            }
            return Errand;
        }

        [HttpPost]
        public async Task<IActionResult> PostErrand(Errand Errand) 
        {
            context.Errands.Update(Errand);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Errand>> DeleteErrand(Guid id)
        {
            var Errand = await context.Errands.FindAsync(id);
            if(Errand==null) 
            {
                return NotFound();
            }
            context.Errands.Remove(Errand);
            await context.SaveChangesAsync();
            return Errand;
        }        
        
    }
    
}