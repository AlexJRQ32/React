using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Categoria> List()
        {
            return _context.Categorias.ToList();
        }

        [HttpPut("Create")]
        public string Create(Categoria temp)
        {
            string msj = "";
            try
            {
                _context.Categorias.Add(temp);
                _context.SaveChanges();
                msj = $"Categoria {temp.Nombre} almacenada correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(Categoria temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (categoria != null)
                    {
                        categoria.Nombre = temp.Nombre;
                        _context.Categorias.Update(categoria);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente a la categoria {categoria.Nombre}";
                    }
                    else
                    {
                        msj = $"Error la categoria {temp.Nombre} no esta registrada";
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
        public Categoria Search(string nombre)
        {
            Categoria temp = null;
            try
            {
                temp = _context.Categorias.FirstOrDefault(y => y.Nombre.Equals(nombre));
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
                Categoria temp = await _context.Categorias.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.Categorias.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminada categoria {temp.Nombre} correctamente..";
                }
                else
                {
                    return msj = $"No existe la categoria con el id {id}";
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