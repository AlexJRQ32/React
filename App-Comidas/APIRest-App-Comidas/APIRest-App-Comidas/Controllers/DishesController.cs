using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RappiDozApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DishesController(AppDbContext context) { _context = context; }

        // GET: api/Dishes (READ ALL)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetAll()
        {
            return await _context.Dishes.Include(d => d.Category).Include(d => d.Restaurant).ToListAsync();
        }

        // GET: api/Dishes/5 (READ BY ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetById(int id)
        {
            var dish = await _context.Dishes.Include(d => d.Category).Include(d => d.Restaurant)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dish == null) return NotFound($"No se encontró el platillo con ID {id}.");
            return Ok(dish);
        }

        // POST: api/Dishes (CREATE)
        [HttpPost]
        public async Task<ActionResult<Dish>> Create([FromBody] Dish dish)
        {
            if (dish == null) return BadRequest("Datos inválidos.");

            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dish.Id }, dish);
        }

        // PUT: api/Dishes/5 (UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Dish updated)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null) return NotFound($"No se encontró el platillo con ID {id}.");

            dish.Name = updated.Name;
            dish.CategoryId = updated.CategoryId;
            dish.Price = updated.Price;
            dish.Img = updated.Img;
            dish.Description = updated.Description;
            dish.RestaurantId = updated.RestaurantId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Dishes/5 (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null) return NotFound($"No se encontró el platillo con ID {id}.");

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}