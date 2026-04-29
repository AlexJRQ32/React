using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public RolesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Rol> List()
        {
            return _context.Roles.ToList();
        }

        [HttpPut("Create")]
        public string Create(Rol temp)
        {
            string msj = "";
            try
            {
                _context.Roles.Add(temp);
                _context.SaveChanges();
                msj = $"Rol {temp.NombreRol} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(Rol temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Rol rol = await _context.Roles.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (rol != null)
                    {
                        rol.NombreRol = temp.NombreRol;
                        _context.Roles.Update(rol);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al rol {rol.NombreRol}";
                    }
                    else
                    {
                        msj = $"Error el rol {temp.NombreRol} no esta registrado";
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
        public Rol Search(string nombreRol)
        {
            Rol temp = null;
            try
            {
                temp = _context.Roles.FirstOrDefault(y => y.NombreRol.Equals(nombreRol));
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
                Rol temp = await _context.Roles.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.Roles.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado rol {temp.NombreRol} correctamente..";
                }
                else
                {
                    return msj = $"No existe el rol con el id {id}";
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