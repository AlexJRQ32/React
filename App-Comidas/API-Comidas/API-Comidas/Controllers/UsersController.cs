using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Comidas.Data;
using Microsoft.EntityFrameworkCore;
using API_Comidas.Models;

namespace API_Comidas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<User>>> List()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<User>> Update(User user)
        {
            try
            {
                if (user == null) return BadRequest("Invalid data.");

                User us = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

                if (us == null) return NotFound("User not found.");

                us.Name = user.Name;
                us.Email = user.Email;
                us.Password = user.Password;
                us.RoleId = user.RoleId;
                us.Phone = user.Phone;
                us.Img = user.Img;

                await _context.SaveChangesAsync();
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating user: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var us = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (us == null) return NotFound("User not found.");

                _context.Users.Remove(us);

                await _context.SaveChangesAsync();

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting user: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null) return NotFound("User not found.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving user: {ex.Message}");
            }
        }

        // Additional method to get address by user ID
        [HttpGet("{Id}/adresses")]
        public async Task<ActionResult<List<Address>>> GetAddressesByUserId(int Id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Addresses).FirstOrDefaultAsync(x => x.Id == Id);
                if (user == null) return NotFound("User not found.");
                return Ok(user.Addresses);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving addresses: {ex.Message}");
            }
        }
    }
}