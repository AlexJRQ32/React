using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UbicacionUsuariosController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public UbicacionUsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<UbicacionUsuario> List()
        {
            return _context.UbicacionUsuario.ToList();
        }

        [HttpPut("Create")]
        public string Create(UbicacionUsuario temp)
        {
            string msj = "";
            try
            {
                _context.UbicacionUsuario.Add(temp);
                _context.SaveChanges();
                msj = $"Ubicacion {temp.NombreUbicacion} almacenada correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(UbicacionUsuario temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    UbicacionUsuario ubicacion = await _context.UbicacionUsuario.FirstOrDefaultAsync(x => x.IdUbicacion == temp.IdUbicacion);
                    if (ubicacion != null)
                    {
                        ubicacion.NombreUbicacion = temp.NombreUbicacion;
                        ubicacion.Latitud = temp.Latitud;
                        ubicacion.Longitud = temp.Longitud;
                        ubicacion.EsPrincipal = temp.EsPrincipal;
                        ubicacion.IdUsuario = temp.IdUsuario;
                        _context.UbicacionUsuario.Update(ubicacion);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente a la ubicacion {ubicacion.NombreUbicacion}";
                    }
                    else
                    {
                        msj = $"Error la ubicacion {temp.NombreUbicacion} no esta registrada";
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
        public UbicacionUsuario Search(string nombreUbicacion)
        {
            UbicacionUsuario temp = null;
            try
            {
                temp = _context.UbicacionUsuario.FirstOrDefault(y => y.NombreUbicacion.Equals(nombreUbicacion));
                return temp;
            }
            catch (Exception ex)
            {
                return temp;
            }
        }

        [HttpDelete("Delete")]
        public async Task<string> Delete(int idUbicacion)
        {
            string msj = "";
            try
            {
                UbicacionUsuario temp = await _context.UbicacionUsuario.FirstOrDefaultAsync(j => j.IdUbicacion == idUbicacion);
                if (temp != null)
                {
                    _context.UbicacionUsuario.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminada ubicacion {temp.NombreUbicacion} correctamente..";
                }
                else
                {
                    return msj = $"No existe la ubicacion con el id {idUbicacion}";
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