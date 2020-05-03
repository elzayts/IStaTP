using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using istatp_lab2.Models;

namespace istatp_lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly Lab2Context _context;

        public ClientsController(Lab2Context context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clients>> GetClients(int id)
        {
            var clients = await _context.Clients.FindAsync(id);

            if (clients == null)
            {
                return NotFound();
            }

            return clients;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClients(int id, Clients clients)
        {
            if (id != clients.ClientID)
            {
                return BadRequest();
            }

            var t = _context.Clients.ToList();
            foreach (var tt in t)
            {
                if (tt.Email == clients.Email)
                {
                    return BadRequest("you're not allowed to create duplicate records");
                }
                
            }

            _context.Entry(clients).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientsExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Clients>> PostClients(Clients clients)
        {
            var t = _context.Clients.ToList();
            foreach (var tt in t)
            {
                if (tt.Email.ToLower().StartsWith(clients.Email.ToLower()))
                {
                    return BadRequest("you're not allowed to create duplicate records");
                }

            }
            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClients", new { id = clients.ClientID }, clients);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clients>> DeleteClients(int id)
        {
            var clients = await _context.Clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }
           

            var ord1 = _context.ClientRoom.Where(a => a.ClientID == id).ToArray();
            foreach (var a in ord1)
            {
                var ord = _context.Order_ClientRoom.Where(b => b.ClientRoomId == a.ClientRoomID).ToArray();
                foreach(var aa in ord)
                {
                    _context.Order_ClientRoom.Remove(aa);
                }
                await _context.SaveChangesAsync();
                _context.ClientRoom.Remove(a);
            }
            await _context.SaveChangesAsync();

            _context.Clients.Remove(clients);
            await _context.SaveChangesAsync();

            return clients;
        }

        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
    }
}
