using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using RappiDozApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context = null;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public List<Pedido> List()
        {
            return _context.Pedidos.ToList();
        }

        [HttpPut("Create")]
        public string Create(Pedido temp)
        {
            string msj = "";
            try
            {
                _context.Pedidos.Add(temp);
                _context.SaveChanges();
                msj = $"Pedido {temp.Id} almacenado correctamente";
                return msj;
            }
            catch (Exception ex)
            {
                msj = $"Error {ex.InnerException.ToString()}";
                return msj;
            }
        }

        [HttpPost("Update")]
        public async Task<string> Update(Pedido temp)
        {
            string msj = "";
            try
            {
                if (temp != null)
                {
                    Pedido pedido = await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == temp.Id);
                    if (pedido != null)
                    {
                        pedido.UsuarioId = temp.UsuarioId;
                        pedido.RestauranteId = temp.RestauranteId;
                        pedido.CuponId = temp.CuponId;
                        pedido.FechaHora = temp.FechaHora;
                        pedido.Total = temp.Total;
                        pedido.Estado = temp.Estado;
                        pedido.EntregaLatitud = temp.EntregaLatitud;
                        pedido.EntregaLongitud = temp.EntregaLongitud;
                        pedido.MetodoPagoId = temp.MetodoPagoId;
                        pedido.MontoDescuento = temp.MontoDescuento;
                        _context.Pedidos.Update(pedido);
                        await _context.SaveChangesAsync();
                        return msj = $"Cambios aplicados correctamente al pedido {pedido.Id}";
                    }
                    else
                    {
                        msj = $"Error el pedido {temp.Id} no esta registrado";
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
        public Pedido Search(int id)
        {
            Pedido temp = null;
            try
            {
                temp = _context.Pedidos.FirstOrDefault(y => y.Id == id);
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
                Pedido temp = await _context.Pedidos.FirstOrDefaultAsync(j => j.Id == id);
                if (temp != null)
                {
                    _context.Pedidos.Remove(temp);
                    await _context.SaveChangesAsync();
                    msj = $"Eliminado pedido {temp.Id} correctamente..";
                }
                else
                {
                    return msj = $"No existe el pedido con el id {id}";
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