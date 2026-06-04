using API_Comidas.Data;
using Microsoft.AspNetCore.Mvc;

namespace API_Comidas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }


    }
}
