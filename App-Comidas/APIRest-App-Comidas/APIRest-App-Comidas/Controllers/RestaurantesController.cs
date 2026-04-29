using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantesController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public RestaurantesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Restaurante> List()
        {
            return _context.Restaurantes.ToList();
        }

        [HttpPut("Create")]
        public string Create(Restaurante temp)
        {
            string msj = "";
            try
            {
                _context.Restaurantes.Add(temp);
                _context.SaveChanges();
                msj = $"Restaurante {temp.NombreComercial} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(Restaurante temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Restaurante restaurante = await _context.Restaurantes.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (restaurante != null)
                    {
                        restaurante.NombreComercial = temp.NombreComercial;
                        restaurante.Direccion = temp.Direccion;
                        restaurante.CategoriaId = temp.CategoriaId;
                        restaurante.HoraApertura = temp.HoraApertura;
                        restaurante.HoraCierre = temp.HoraCierre;
                        restaurante.Latitud = temp.Latitud;
                        restaurante.Longitud = temp.Longitud;
                        restaurante.UsuarioId = temp.UsuarioId;
                        _context.Restaurantes.Update(restaurante);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al restaurante {restaurante.NombreComercial}";
                    }
                    else
                    {
                        msj = $"Error el restaurante {temp.NombreComercial} no esta registrado";
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
        public Restaurante Search(string nombreComercial)
        {
            Restaurante temp = null;
            try
            {
                temp = _context.Restaurantes.FirstOrDefault(y => y.NombreComercial.Equals(nombreComercial));
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
                Restaurante temp = await _context.Restaurantes.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.Restaurantes.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado restaurante {temp.NombreComercial} correctamente..";
                }
                else
                {
                    return msj = $"No existe el restaurante con el id {id}";
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