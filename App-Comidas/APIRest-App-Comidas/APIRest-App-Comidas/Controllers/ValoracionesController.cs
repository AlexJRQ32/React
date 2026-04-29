using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValoracionesController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public ValoracionesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Valoracion> List()
        {
            return _context.Valoraciones.ToList();
        }

        [HttpPut("Create")]
        public string Create(Valoracion temp)
        {
            string msj = "";
            try
            {
                _context.Valoraciones.Add(temp);
                _context.SaveChanges();
                msj = $"Valoracion almacenada correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(Valoracion temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Valoracion valoracion = await _context.Valoraciones.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (valoracion != null)
                    {
                        valoracion.Estrellas = temp.Estrellas;
                        valoracion.Comentario = temp.Comentario;
                        valoracion.Recomendacion = temp.Recomendacion;
                        valoracion.Fecha = temp.Fecha;
                        valoracion.RestauranteId = temp.RestauranteId;
                        valoracion.UsuarioId = temp.UsuarioId;
                        _context.Valoraciones.Update(valoracion);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente a la valoracion";
                    }
                    else
                    {
                        msj = $"Error la valoracion no esta registrada";
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
        public Valoracion Search(int id)
        {
            Valoracion temp = null;
            try
            {
                temp = _context.Valoraciones.FirstOrDefault(y => y.Id == id);
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
                Valoracion temp = await _context.Valoraciones.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.Valoraciones.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminada valoracion correctamente..";
                }
                else
                {
                    return msj = $"No existe la valoracion con el id {id}";
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