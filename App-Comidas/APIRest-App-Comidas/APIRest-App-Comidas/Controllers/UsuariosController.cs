using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Usuario> List()
        {
            return _context.Usuarios.ToList();
        }

        [HttpPut("Create")]
        public string Create(Usuario temp)
        {
            string msj = "";
            try
            {
                _context.Usuarios.Add(temp);
                _context.SaveChanges();
                msj = $"Usuario {temp.Email} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(Usuario temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (usuario != null)
                    {
                        usuario.NombreCompleto = temp.NombreCompleto;
                        usuario.Email = temp.Email;
                        usuario.PasswordHash = temp.PasswordHash;
                        usuario.Telefono = temp.Telefono;
                        usuario.RolId = temp.RolId;
                        usuario.Activo = temp.Activo;
                        _context.Usuarios.Update(usuario);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al usuario {usuario.Email}";
                    }
                    else
                    {
                        msj = $"Error el usuario {temp.Email} no esta registrado";
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
        public Usuario Search(string email)
        {
            Usuario temp = null;
            try
            {
                temp = _context.Usuarios.FirstOrDefault(y => y.Email.Equals(email));
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
                Usuario temp = await _context.Usuarios.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.Usuarios.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado usuario {temp.Email} correctamente..";
                }
                else
                {
                    return msj = $"No existe el usuario con el id {id}";
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