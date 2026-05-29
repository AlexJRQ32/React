using APIRest_App_Comidas.Data;
using APIRest_App_Comidas.Models;
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
    public class CouponsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CouponsController(AppDbContext context) { _context = context; }

        // GET: api/Coupons (READ ALL)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetAll()
        {
            return await _context.Coupons.Include(c => c.Category).Include(c => c.Order).Include(c => c.User).ToListAsync();
        }

        // GET: api/Coupons/5 (READ BY ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetById(int id)
        {
            var coupon = await _context.Coupons.Include(c => c.Category).Include(c => c.Order).Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coupon == null) return NotFound($"No se encontró el cupón con ID {id}.");
            return Ok(coupon);
        }

        // POST: api/Coupons (CREATE)
        [HttpPost]
        public async Task<ActionResult<Coupon>> Create([FromBody] Coupon coupon)
        {
            if (coupon == null) return BadRequest("Datos del cupón inválidos.");

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = coupon.Id }, coupon);
        }

        // PUT: api/Coupons/5 (UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Coupon updated)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return NotFound($"No se encontró el cupón con ID {id}.");

            coupon.Code = updated.Code;
            coupon.Title = updated.Title;
            coupon.Description = updated.Description;
            coupon.Discount = updated.Discount;
            coupon.IsPercentage = updated.IsPercentage;
            coupon.ExpirationDate = updated.ExpirationDate;
            coupon.Active = updated.Active;
            coupon.Stock = updated.Stock;
            coupon.CategoryId = updated.CategoryId;
            coupon.OrderId = updated.OrderId;
            coupon.UserId = updated.UserId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Coupons/5 (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return NotFound($"No se encontró el cupón con ID {id}.");

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // REQUERIMIENTO EXTRA: Devolver los cupones apartados por usuario
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetApartedByUser(int userId)
        {
            var userCoupons = await _context.Coupons.Where(c => c.UserId == userId).ToListAsync();
            return Ok(userCoupons);
        }

        #region ReservedCoupons
        // 1. GET /api/coupons/available -> Cupones normales libres de asignación en el catálogo global
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetAvailableCoupons()
        {
            // 1. Obtener la lista de IDs de todos los cupones que ya han sido tomados por algún usuario
            var reservedIds = await _context.ReservedCoupons
                .Select(rc => rc.CouponId)
                .ToListAsync();

            // 2. Retornar los cupones del catálogo que estén activos, tengan stock y no pertenezcan a los reservados
            return await _context.Coupons
                .Where(c => c.Active && (c.Stock == null || c.Stock > 0) && !reservedIds.Contains(c.Id))
                .ToListAsync();
        }

        // 2. GET /api/coupons/reserved/{userId} -> Obtener los cupones que tiene en cartera un cliente particular
        [HttpGet("reserved/{userId}")]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetReservedCoupons(int userId)
        {
            // Consultamos la tabla relacional, filtramos por usuario y extraemos directamente el objeto Coupon completo
            return await _context.ReservedCoupons
                .Where(rc => rc.UserId == userId)
                .Include(rc => rc.Coupon)
                .Select(rc => rc.Coupon!)
                .ToListAsync();
        }

        // 3. POST /api/coupons/{couponId}/apartar/{userId} -> Reestructuración de la lógica para usar ReservedCoupons
        [HttpPost("{couponId}/apartar/{userId}")]
        public async Task<IActionResult> ApartarCoupon(int couponId, int userId)
        {
            var coupon = await _context.Coupons.FindAsync(couponId);
            if (coupon == null)
            {
                return NotFound("El cupón promocional especificado no existe.");
            }

            // Validar que el cupón no esté dado de baja o agotado
            if (!coupon.Active || (coupon.Stock.HasValue && coupon.Stock <= 0))
            {
                return BadRequest("El cupón seleccionado no está activo o se ha quedado sin stock.");
            }

            // Validar seguridad: Evitar que el mismo usuario guarde dos veces el mismo cupón en su cartera
            bool yaReservado = await _context.ReservedCoupons
                .AnyAsync(rc => rc.CouponId == couponId && rc.UserId == userId);

            if (yaReservado)
            {
                return BadRequest("Este cupón promocional ya se encuentra registrado en tu wallet.");
            }

            // Si el cupón posee un límite físico de existencias, restamos una unidad
            if (coupon.Stock.HasValue)
            {
                coupon.Stock--;
            }

            // Registrar la relación en la nueva tabla intermedia
            var reservation = new ReservedCoupon
            {
                CouponId = couponId,
                UserId = userId,
                ReservedAt = DateTime.UtcNow
            };

            _context.ReservedCoupons.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok(new { message = "¡Cupón agregado a tu billetera promocional con éxito!" });
        }
        #endregion
    }
}