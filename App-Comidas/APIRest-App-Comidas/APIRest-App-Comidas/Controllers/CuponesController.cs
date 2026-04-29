using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuponesController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        //Constructor
        public CuponesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Cupon> List()
        {
            return _context.Cupones.ToList();//Se envia la lista
        }

        //Metodo encargado de crear un cupon
        [HttpPut("Create")]
        public string Create(Cupon temp)
        {
            string msj = "";
            try
            {
                //Se almacena el cupon
                _context.Cupones.Add(temp);

                //Aplicar los cambios en la DB
                _context.SaveChanges();

                msj = $"Cupon {temp.Titulo} almacenado correctamente";

                return msj;
            }//En caso de error se captura
            catch (Exception ex)
            {   //Se personaliza el error
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
                //Y se retorna
            }
        }
        //Metodo de modificar los datos
        [HttpPost("Update")]
        public async Task<string> Update(Cupon temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Cupon cupon = await _context.Cupones.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (cupon != null)
                    {
                        cupon.Titulo = temp.Titulo;
                        cupon.Descripcion = temp.Descripcion;
                        cupon.Descuento = temp.Descuento;
                        cupon.EsPorcentaje = temp.EsPorcentaje;
                        cupon.FechaExpiracion = temp.FechaExpiracion;
                        cupon.Activo = temp.Activo;
                        cupon.Stock = temp.Stock;
                        cupon.CategoriaId = temp.CategoriaId;
                        _context.Cupones.Update(cupon);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al cupon {cupon.Titulo}";
                    }
                    else
                    {
                        msj = $"Error el cupon {temp.Titulo} no esta registrado";
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
        public Cupon Search(string name)
        {
            Cupon temp = null;
            try
            {
                temp = _context.Cupones.FirstOrDefault(y => y.Titulo.Equals(name));
                return temp;
            }
            catch (Exception ex)
            {
                return temp;
            }
        }

        [HttpDelete("Delete")]
        public async Task<string> Delete(string code)
        {
            string msj = "";
            try
            {
                Cupon temp = await _context.Cupones.FirstOrDefaultAsync(j => j.Id.Equals(code));

                if (temp != null)
                {
                    _context.Cupones.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado cupon {temp.Titulo} correctamente..";
                }
                else
                {
                    return msj = $"No existe el cupon con el codigo {code}";
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
