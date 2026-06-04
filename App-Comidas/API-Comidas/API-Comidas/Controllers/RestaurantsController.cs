using API_Comidas.Data;
using API_Comidas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RestaurantsController> _logger;

        public RestaurantsController(AppDbContext context, ILogger<RestaurantsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            try
            {
                var restaurants = await _context.Restaurants
                    .Include(r => r.Category)
                    .Include(r => r.User)
                    .Include(r => r.Dishes)
                    .Include(r => r.Coupons)
                    .ToListAsync();

                _logger.LogInformation($"Retrieved {restaurants.Count} restaurants");
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving restaurants");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            try
            {
                var restaurant = await _context.Restaurants
                    .Include(r => r.Category)
                    .Include(r => r.User)
                    .Include(r => r.Dishes)
                    .Include(r => r.Coupons)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (restaurant == null)
                {
                    return NotFound(new { message = $"Restaurant with ID {id} not found" });
                }

                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving restaurant {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] Restaurant restaurant)
        {
            try
            {
                if (restaurant == null)
                {
                    return BadRequest(new { message = "Restaurant cannot be null" });
                }

                if (string.IsNullOrWhiteSpace(restaurant.TradeName))
                {
                    return BadRequest(new { message = "TradeName is required" });
                }

                if (restaurant.CategoryId <= 0)
                {
                    return BadRequest(new { message = "CategoryId must be valid" });
                }

                if (restaurant.UserId <= 0)
                {
                    return BadRequest(new { message = "UserId must be valid" });
                }

                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == restaurant.CategoryId);
                if (!categoryExists)
                {
                    return BadRequest(new { message = $"Category with ID {restaurant.CategoryId} does not exist" });
                }

                var userExists = await _context.Users.AnyAsync(u => u.Id == restaurant.UserId);
                if (!userExists)
                {
                    return BadRequest(new { message = $"User with ID {restaurant.UserId} does not exist" });
                }

                _context.Restaurants.Add(restaurant);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Restaurant created: {restaurant.Id}");
                return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error creating restaurant");
                return StatusCode(500, new { message = "Database error", error = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating restaurant");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            try
            {
                if (id != restaurant.Id)
                {
                    return BadRequest(new { message = "ID mismatch" });
                }

                _context.Entry(restaurant).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Restaurant updated: {id}");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Error updating restaurant {id}");
                return StatusCode(500, new { message = "Concurrency error", error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating restaurant {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                var restaurant = await _context.Restaurants.FindAsync(id);
                if (restaurant == null)
                {
                    return NotFound(new { message = $"Restaurant with ID {id} not found" });
                }

                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Restaurant deleted: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting restaurant {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
