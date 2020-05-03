    
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
    public class Order_ClientRoomController : ControllerBase
    {
        private readonly Lab2Context _context;

        public Order_ClientRoomController(Lab2Context context)
        {
            _context = context;
        }

        // GET: api/Order_ClientRoom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order_ClientRoom>>> GetOrder_ClientRoom()
        {
            return await _context.Order_ClientRoom.ToListAsync();
        }

        // GET: api/Order_ClientRoom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order_ClientRoom>> GetOrder_ClientRoom(int id)
        {
            var order_ClientRoom = await _context.Order_ClientRoom.FindAsync(id);

            if (order_ClientRoom == null)
            {
                return NotFound();
            }

            return order_ClientRoom;
        }

        // PUT: api/Order_ClientRoom/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder_ClientRoom(int id, Order_ClientRoom order_ClientRoom)
        {
            if (id != order_ClientRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(order_ClientRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_ClientRoomExists(id))
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

        // POST: api/Order_ClientRoom
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order_ClientRoom>> PostOrder_ClientRoom(Order_ClientRoom order_ClientRoom)
        {
            var t = _context.Order_ClientRoom.ToList();
            foreach (var tt in t)
            {
                if (tt.ClientRoomId == order_ClientRoom.ClientRoomId && tt.OrderId == order_ClientRoom.OrderId)
                {
                    return BadRequest("you're not allowed to create duplicate records");
                }

            }

            if (_context.Order_ClientRoom.Where(a => a.ClientRoomId == order_ClientRoom.ClientRoomId).Count() >= 5)
            {
                return BadRequest("5 orders max");
            }

            _context.Order_ClientRoom.Add(order_ClientRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_ClientRoom", new { id = order_ClientRoom.Id }, order_ClientRoom);
        }

        // DELETE: api/Order_ClientRoom/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order_ClientRoom>> DeleteOrder_ClientRoom(int id)
        {
            var order_ClientRoom = await _context.Order_ClientRoom.FindAsync(id);
            if (order_ClientRoom == null)
            {
                return NotFound();
            }

            _context.Order_ClientRoom.Remove(order_ClientRoom);
            await _context.SaveChangesAsync();

            return order_ClientRoom;
        }

        private bool Order_ClientRoomExists(int id)
        {
            return _context.Order_ClientRoom.Any(e => e.Id == id);
        }
    }
}
