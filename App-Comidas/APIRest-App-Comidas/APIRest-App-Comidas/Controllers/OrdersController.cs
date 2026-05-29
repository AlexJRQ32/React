using APIRest_App_Comidas.Data;   // Asegúrate de que coincida con tu namespace del DbContext
using APIRest_App_Comidas.Models; // Asegúrate de que coincida con tu namespace de modelos
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RappiDozApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest_App_Comidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // 1. POST /api/orders -> Crear un pedido desde el carrito (Checkout)
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            if (order == null || order.Items == null || !order.Items.Any())
            {
                return BadRequest("El pedido no contiene productos o la estructura enviada es inválida.");
            }

            // Automatizar datos de tiempo según los requerimientos del modelo actual
            order.Date = DateTime.Now.ToString("yyyy-MM-dd");
            order.Time = DateTime.Now.ToString("HH:mm:ss");
            order.Status = "PENDING"; // Estado inicial por defecto

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Retorna el objeto creado con código HTTP 201 Created y su URL correspondiente
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // 2. GET /api/orders -> Listar todos los pedidos (Supervisión / Administración General)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        // 3. GET /api/orders/{id} -> Obtener el detalle específico de una sola orden por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound($"No se encontró ningún pedido con el ID {id}.");
            }

            return order;
        }

        // 4. GET /api/orders/user/{userId} -> Historial de pedidos de un cliente específico
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(int userId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == userId)
                .Include(o => o.Items)
                .OrderByDescending(o => o.Id) // Las compras más recientes aparecen primero
                .ToListAsync();
        }

        // 5. GET /api/orders/restaurant/{restaurantId} -> Órdenes entrantes de un restaurante específico
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByRestaurant(int restaurantId)
        {
            return await _context.Orders
                .Where(o => o.RestaurantId == restaurantId)
                .Include(o => o.Items)
                .OrderByDescending(o => o.Id) // Los pedidos nuevos aparecen al principio de la lista
                .ToListAsync();
        }

        // 6. PUT /api/orders/{id} -> Actualizar el estado del pedido (PENDING, DELIVERED, CANCELLED)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string newStatus)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound($"No se encontró ningún pedido con el ID {id}.");
            }

            if (string.IsNullOrWhiteSpace(newStatus))
            {
                return BadRequest("El nuevo estado proporcionado no puede estar vacío.");
            }

            order.Status = newStatus.ToUpper(); // Guardar en mayúsculas estandarizadas
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Orders.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent(); // Retorna 204 indicando actualización exitosa sin cuerpo de respuesta
        }
    }
}