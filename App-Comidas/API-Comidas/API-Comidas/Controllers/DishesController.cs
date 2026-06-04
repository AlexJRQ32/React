using API_Comidas.Data;
using API_Comidas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DishesController> _logger;

        public DishesController(AppDbContext context, ILogger<DishesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
        {
            try
            {
                var dishes = await _context.Dishes
                    .Include(d => d.Restaurant)
                    .ToListAsync();

                _logger.LogInformation($"Retrieved {dishes.Count} dishes");
                return Ok(dishes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dishes");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
            try
            {
                var dish = await _context.Dishes
                    .Include(d => d.Restaurant)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (dish == null)
                {
                    return NotFound(new { message = $"Dish with ID {id} not found" });
                }

                return Ok(dish);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving dish {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Dish>> CreateDish([FromBody] Dish dish)
        {
            try
            {
                if (dish == null)
                {
                    return BadRequest(new { message = "Dish cannot be null" });
                }

                if (string.IsNullOrWhiteSpace(dish.Name))
                {
                    return BadRequest(new { message = "Name is required" });
                }

                if (dish.RestaurantId <= 0)
                {
                    return BadRequest(new { message = "RestaurantId must be valid" });
                }

                var restaurantExists = await _context.Restaurants.AnyAsync(r => r.Id == dish.RestaurantId);
                if (!restaurantExists)
                {
                    return BadRequest(new { message = $"Restaurant with ID {dish.RestaurantId} does not exist" });
                }

                _context.Dishes.Add(dish);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Dish created: {dish.Id}");
                return CreatedAtAction(nameof(GetDish), new { id = dish.Id }, dish);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error creating dish");
                return StatusCode(500, new { message = "Database error", error = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating dish");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDish(int id, [FromBody] Dish dish)
        {
            try
            {
                if (id != dish.Id)
                {
                    return BadRequest(new { message = "ID mismatch" });
                }

                _context.Entry(dish).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Dish updated: {id}");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Error updating dish {id}");
                return StatusCode(500, new { message = "Concurrency error", error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating dish {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            try
            {
                var dish = await _context.Dishes.FindAsync(id);
                if (dish == null)
                {
                    return NotFound(new { message = $"Dish with ID {id} not found" });
                }

                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Dish deleted: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting dish {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
