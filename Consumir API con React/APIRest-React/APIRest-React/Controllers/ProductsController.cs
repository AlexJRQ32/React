using APIRest_React.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIRest_React.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<Products>>> List()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Products>> Create(Products product)
        {
            try
            {
                product.Activo = true;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok(product);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el producto: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Products product)
        {
            try
            {
                if (product == null) return BadRequest("Datos no válidos");

                Products prod = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

                if (prod == null) return NotFound("Producto no encontrado");

                prod.Nombre = product.Nombre;
                prod.Precio = product.Precio;
                prod.Stock = product.Stock;
                prod.Activo = product.Activo ?? prod.Activo;

                await _context.SaveChangesAsync();
                return Ok("Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el producto: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound("El producto no existe.");
                }

                _context.Products.Remove(product);

                await _context.SaveChangesAsync();

                return Ok("Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"No se pudo eliminar: {ex.Message}");
            }
        }

        [HttpGet("SearchName/{name}")]
        public async Task<ActionResult<List<Products>>> SearchName(string name)
        {
            try
            {
                var result = await _context.Products
                    .Where(x => x.Nombre.Contains(name))
                    .ToListAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
