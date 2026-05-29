using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RappiDozApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralDataController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GeneralDataController(AppDbContext context) { _context = context; }

        // REQUERIMIENTO 1: Categorías - Devolver todas
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories() =>
            await _context.Categories.ToListAsync();

        // REQUERIMIENTO 7: Métodos de Pago - Catálogo
        [HttpGet("payment-methods")]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods() =>
            await _context.PaymentMethods.ToListAsync();
    }
}