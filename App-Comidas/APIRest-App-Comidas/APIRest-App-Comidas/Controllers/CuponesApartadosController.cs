using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuponesApartadosController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public CuponesApartadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<CuponApartado> List()
        {
            return _context.CuponesApartados.ToList();
        }

        [HttpPut("Create")]
        public string Create(CuponApartado temp)
        {
            string msj = "";
            try
            {
                _context.CuponesApartados.Add(temp);
                _context.SaveChanges();
                msj = $"Cupon apartado {temp.Codigo} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(CuponApartado temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    CuponApartado cupon = await _context.CuponesApartados.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (cupon != null)
                    {
                        cupon.UsuarioEmail = temp.UsuarioEmail;
                        cupon.Codigo = temp.Codigo;
                        cupon.Descuento = temp.Descuento;
                        cupon.EsPorcentaje = temp.EsPorcentaje;
                        cupon.FechaReclamado = temp.FechaReclamado;
                        _context.CuponesApartados.Update(cupon);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al cupon apartado {cupon.Codigo}";
                    }
                    else
                    {
                        msj = $"Error el cupon apartado {temp.Codigo} no esta registrado";
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
        public CuponApartado Search(string codigo)
        {
            CuponApartado temp = null;
            try
            {
                temp = _context.CuponesApartados.FirstOrDefault(y => y.Codigo.Equals(codigo));
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
                CuponApartado temp = await _context.CuponesApartados.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.CuponesApartados.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado cupon apartado {temp.Codigo} correctamente..";
                }
                else
                {
                    return msj = $"No existe el cupon apartado con el id {id}";
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