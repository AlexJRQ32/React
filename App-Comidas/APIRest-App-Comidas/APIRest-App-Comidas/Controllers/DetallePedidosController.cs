using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetallePedidosController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public DetallePedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<DetallePedido> List()
        {
            return _context.DetallePedidos.ToList();
        }

        [HttpPut("Create")]
        public string Create(DetallePedido temp)
        {
            string msj = "";
            try
            {
                _context.DetallePedidos.Add(temp);
                _context.SaveChanges();
                msj = $"Detalle de pedido {temp.Id} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(DetallePedido temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    DetallePedido detalle = await _context.DetallePedidos.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (detalle != null)
                    {
                        detalle.PedidoId = temp.PedidoId;
                        detalle.ProductoId = temp.ProductoId;
                        detalle.Cantidad = temp.Cantidad;
                        detalle.PrecioHistorico = temp.PrecioHistorico;
                        _context.DetallePedidos.Update(detalle);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al detalle {detalle.Id}";
                    }
                    else
                    {
                        msj = $"Error el detalle de pedido {temp.Id} no esta registrado";
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
        public DetallePedido Search(int id)
        {
            DetallePedido temp = null;
            try
            {
                temp = _context.DetallePedidos.FirstOrDefault(y => y.Id == id);
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
                DetallePedido temp = await _context.DetallePedidos.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.DetallePedidos.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado detalle de pedido {temp.Id} correctamente..";
                }
                else
                {
                    return msj = $"No existe el detalle de pedido con el id {id}";
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