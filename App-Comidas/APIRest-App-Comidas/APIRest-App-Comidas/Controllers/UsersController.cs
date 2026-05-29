using APIRest_App_Comidas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RappiDozApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest_App_Comidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) { _context = context; }

        // GET: api/Users (READ ALL)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        // GET: api/Users/5 (READ BY ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound($"No se encontró el usuario con ID {id}.");
            return Ok(user);
        }

        // POST: api/Users (CREATE)
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (user == null) return BadRequest("Datos del usuario inválidos.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: api/Users/5 (UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User updated)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound($"No se encontró el usuario con ID {id}.");

            user.Name = updated.Name;
            user.Email = updated.Email;
            user.Img = updated.Img;
            user.Password = updated.Password;
            user.Phone = updated.Phone;
            user.RoleId = updated.RoleId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Users/5 (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound($"No se encontró el usuario con ID {id}.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // REQUERIMIENTO EXTRA: Devolver las ubicaciones filtradas por usuario
        [HttpGet("{userId}/addresses")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddressesByUser(int userId)
        {
            var addresses = await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
            return Ok(addresses);
        }
    }
}