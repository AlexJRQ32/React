using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetodosPagoController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public MetodosPagoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<MetodoPago> List()
        {
            return _context.MetodosPago.ToList();
        }

        [HttpPut("Create")]
        public string Create(MetodoPago temp)
        {
            string msj = "";
            try
            {
                _context.MetodosPago.Add(temp);
                _context.SaveChanges();
                msj = $"Metodo de pago {temp.Nombre} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(MetodoPago temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    MetodoPago metodo = await _context.MetodosPago.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (metodo != null)
                    {
                        metodo.Nombre = temp.Nombre;
                        _context.MetodosPago.Update(metodo);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al metodo de pago {metodo.Nombre}";
                    }
                    else
                    {
                        msj = $"Error el metodo de pago {temp.Nombre} no esta registrado";
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
        public MetodoPago Search(string nombre)
        {
            MetodoPago temp = null;
            try
            {
                temp = _context.MetodosPago.FirstOrDefault(y => y.Nombre.Equals(nombre));
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
                MetodoPago temp = await _context.MetodosPago.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.MetodosPago.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado metodo de pago {temp.Nombre} correctamente..";
                }
                else
                {
                    return msj = $"No existe el metodo de pago con el id {id}";
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