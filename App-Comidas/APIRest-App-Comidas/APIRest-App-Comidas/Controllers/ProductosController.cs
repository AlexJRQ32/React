using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Producto> List()
        {
            return _context.Productos.ToList();
        }

        [HttpPut("Create")]
        public string Create(Producto temp)
        {
            string msj = "";
            try
            {
                _context.Productos.Add(temp);
                _context.SaveChanges();
                msj = $"Producto {temp.Nombre} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(Producto temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Producto producto = await _context.Productos.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (producto != null)
                    {
                        producto.Nombre = temp.Nombre;
                        producto.Descripcion = temp.Descripcion;
                        producto.Precio = temp.Precio;
                        producto.RestauranteId = temp.RestauranteId;
                        producto.CategoriaId = temp.CategoriaId;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al producto {producto.Nombre}";
                    }
                    else
                    {
                        msj = $"Error el producto {temp.Nombre} no esta registrado";
                    }
                }
                else
                {
                    msj = "No hay datos";
                }
                return msj;
            }
            catch (Exception ex)
            {
                return msj = $"Error {ex.InnerException.ToString()}";
            }
        }

        [HttpGet("Search")]
        public Producto Search(string nombre)
        {
            Producto temp = null;
            try
            {
                temp = _context.Productos.FirstOrDefault(y => y.Nombre.Equals(nombre));
                return temp;
            }
            catch (Exception ex)
            {
                return temp;
            }
        }

        [HttpDelete("Delete")]
        public async Task<string> Delete(int id)
        {
            string msj = "";
            try
            {
                Producto temp = await _context.Productos.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.Productos.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado producto {temp.Nombre} correctamente..";
                }
                else
                {
                    return msj = $"No existe el producto con el id {id}";
                }
                return msj;
            }
            catch (Exception ex)
            {
                return msj = $"Error {ex.InnerException.ToString()}";
            }
        }
    }
}