using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryWebAPI.Models;

namespace InventoryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserInfsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserInfs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInf>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserInfs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInf>> GetUserInf(int id)
        {
            var userInf = await _context.Users.FindAsync(id);

            if (userInf == null)
            {
                return NotFound();
            }

            return userInf;
        }

        // PUT: api/UserInfs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInf(int id, UserInf userInf)
        {
            if (id != userInf.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userInf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserInfs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserInf>> PostUserInf(UserInf userInf)
        {
            _context.Users.Add(userInf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInf", new { id = userInf.UserId }, userInf);
        }

        // DELETE: api/UserInfs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserInf>> DeleteUserInf(int id)
        {
            var userInf = await _context.Users.FindAsync(id);
            if (userInf == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userInf);
            await _context.SaveChangesAsync();

            return userInf;
        }

        private bool UserInfExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
