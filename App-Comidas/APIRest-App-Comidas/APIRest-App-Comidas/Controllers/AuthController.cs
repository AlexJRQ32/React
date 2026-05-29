using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RappiDozApp.Models;
using System.Threading.Tasks;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context) { _context = context; }

        // REQUERIMIENTO 13: Login (Autenticación)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null) return Unauthorized("Credenciales incorrectas.");

            return Ok(new { Message = "Inicio de sesión exitoso", User = user });
        }

        // REQUERIMIENTO 14: Registro Dinámico (Usuario/Restaurante)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("El correo electrónico ya está registrado.");

            // Rol 2 = Business/Restaurante, Rol 3 = Customer/Usuario
            int assignedRoleId = dto.RoleName.ToLower() == "restaurante" ? 2 : 3;

            var user = new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Phone = dto.Phone,
                RoleId = assignedRoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // LÓGICA CONDICIONAL: Si eligió restaurante, se registra automáticamente usando su UserId
            if (assignedRoleId == 2)
            {
                var restaurant = new Restaurant
                {
                    Id = dto.Id,
                    TradeName = dto.TradeName ?? $"Restaurante de {dto.Name}",
                    CategoryId = dto.CategoryId == 0 ? 1 : dto.CategoryId,
                    UserId = user.Id, // Enlace directo por ID de Usuario
                    OpeningTime = dto.OpeningTime ?? "08:00",
                    ClosingTime = dto.ClosingTime ?? "22:00",
                    IsOpen = true,
                    Rating = "5.0"
                };
                _context.Restaurants.Add(restaurant);
                await _context.SaveChangesAsync();
            }

            return Ok(new { Message = "Registro exitoso", UserId = user.Id });
        }
    }
}