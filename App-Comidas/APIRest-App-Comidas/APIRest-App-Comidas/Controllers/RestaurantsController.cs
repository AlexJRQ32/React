using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RappiDozApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RestaurantsController(AppDbContext context) { _context = context; }

        // GET: api/Restaurants (READ ALL)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAll() =>
            await _context.Restaurants.Include(r => r.Category).Include(r => r.User).ToListAsync();

        // GET: api/Restaurants/5 (READ BY ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetById(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null) return NotFound($"No se encontró el restaurante con ID {id}.");
            return Ok(restaurant);
        }

        // POST: api/Restaurants (CREATE)
        [HttpPost]
        public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurant)
        {
            if (restaurant == null) return BadRequest("Datos del restaurante inválidos.");

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }

        // PUT: api/Restaurants/5 (UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Restaurant updated)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return NotFound();

            restaurant.TradeName = updated.TradeName;
            restaurant.Address = updated.Address;
            restaurant.CategoryId = updated.CategoryId;
            restaurant.UserId = updated.UserId; // Mapeado explícito del dueño
            restaurant.OpeningTime = updated.OpeningTime;
            restaurant.ClosingTime = updated.ClosingTime;
            restaurant.Img = updated.Img;
            restaurant.IsOpen = updated.IsOpen;
            restaurant.DeliveryFee = updated.DeliveryFee;
            restaurant.DeliveryTime = updated.DeliveryTime;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Restaurants/5 (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return NotFound();

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // REQUERIMIENTO: Get para devolver productos (menú) conectados a un restaurante específico
        [HttpGet("{id}/menu")]
        public async Task<ActionResult<IEnumerable<Dish>>> GetRestaurantMenu(int id)
        {
            var dishes = await _context.Dishes.Where(d => d.RestaurantId == id).ToListAsync();
            return Ok(dishes);
        }

        // REQUERIMIENTO: Get para devolver restaurantes relacionados a un ID de usuario (dueño)
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantsByUser(int userId)
        {
            var restaurants = await _context.Restaurants.Where(r => r.UserId == userId).ToListAsync();
            return Ok(restaurants);
        }
    }
}