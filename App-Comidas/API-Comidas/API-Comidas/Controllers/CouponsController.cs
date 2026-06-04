using API_Comidas.Data;
using API_Comidas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CouponsController> _logger;

        public CouponsController(AppDbContext context, ILogger<CouponsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
        {
            try
            {
                var coupons = await _context.Coupons
                    .Include(c => c.Restaurant)
                    .Include(c => c.User)
                    .ToListAsync();

                _logger.LogInformation($"Retrieved {coupons.Count} coupons");
                return Ok(coupons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving coupons");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCoupon(int id)
        {
            try
            {
                var coupon = await _context.Coupons
                    .Include(c => c.Restaurant)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (coupon == null)
                {
                    return NotFound(new { message = $"Coupon with ID {id} not found" });
                }

                return Ok(coupon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving coupon {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                if (coupon == null)
                {
                    return BadRequest(new { message = "Coupon cannot be null" });
                }

                if (string.IsNullOrWhiteSpace(coupon.Code))
                {
                    return BadRequest(new { message = "Code is required" });
                }

                if (coupon.Discount <= 0)
                {
                    return BadRequest(new { message = "Discount must be greater than 0" });
                }

                _context.Coupons.Add(coupon);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Coupon created: {coupon.Id}");
                return CreatedAtAction(nameof(GetCoupon), new { id = coupon.Id }, coupon);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error creating coupon");
                return StatusCode(500, new { message = "Database error", error = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating coupon");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] Coupon coupon)
        {
            try
            {
                if (id != coupon.Id)
                {
                    return BadRequest(new { message = "ID mismatch" });
                }

                _context.Entry(coupon).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Coupon updated: {id}");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Error updating coupon {id}");
                return StatusCode(500, new { message = "Concurrency error", error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating coupon {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            try
            {
                var coupon = await _context.Coupons.FindAsync(id);
                if (coupon == null)
                {
                    return NotFound(new { message = $"Coupon with ID {id} not found" });
                }

                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Coupon deleted: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting coupon {id}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
