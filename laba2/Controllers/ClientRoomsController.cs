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
    public class ClientRoomsController : ControllerBase
    { 
        private readonly Lab2Context _context;

        public ClientRoomsController(Lab2Context context)
        {
            _context = context;
        }

        // GET: api/ClientRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientRoom>>> GetClientRoom()
        {
            return await _context.ClientRoom.ToListAsync();
        }

        // GET: api/ClientRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientRoom>> GetClientRoom(int id)
        {
            var clientRoom = await _context.ClientRoom.FindAsync(id);

            if (clientRoom == null)
            {
                return NotFound();
            }

            return clientRoom;
        }

        // PUT: api/ClientRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientRoom(int id, ClientRoom clientRoom)
        {
            if (id != clientRoom.ClientRoomID)
            {
                return BadRequest();
            }
          
    
            _context.Entry(clientRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientRoomExists(id))
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

        // POST: api/ClientRooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ClientRoom>> PostClientRoom(ClientRoom clientRoom)
        {
            switch (Correct(clientRoom))
            {
                case 1:
                    return BadRequest("you're not allowed to create duplicate records");                  
                case 2:
                    return  BadRequest("you're not allowed to create record for room which in use during this dates");
                case 3:
                    return BadRequest("Check out date can't be same or samller then check in date");
                case 4:
                    return  BadRequest("Client doesnt exist");
                case 5:
                    return BadRequest("Room doesnt exist");
            }
            _context.ClientRoom.Add(clientRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientRoom", new { id = clientRoom.ClientRoomID }, clientRoom);
        }

        // DELETE: api/ClientRooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientRoom>> DeleteClientRoom(int id)
        {
            var clientRoom = await _context.ClientRoom.FindAsync(id);
            if (clientRoom == null)
            {
                return NotFound();
            }
            var cl_or = _context.Order_ClientRoom.Where(a => a.ClientRoomId == id).ToArray();
            foreach (var a in cl_or)
            {
                _context.Order_ClientRoom.Remove(a);
            }

            _context.ClientRoom.Remove(clientRoom);
            await _context.SaveChangesAsync();

            return clientRoom;
        }
        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
        private bool ClientRoomExists(int id)
        {
            return _context.ClientRoom.Any(e => e.ClientRoomID == id);
        }

        private int Correct(ClientRoom clientRoom)
        {
            var t = _context.ClientRoom.ToList();
            foreach (var tt in t)
            {
                if (tt.RoomID == clientRoom.RoomID && tt.ClientID == clientRoom.ClientID && tt.Check_inDate == clientRoom.Check_inDate && tt.Check_outDate == clientRoom.Check_outDate)
                {

                    return 1; //BadRequest("you're not allowed to create duplicate records");
                }
                if ((tt.RoomID == clientRoom.RoomID) && ((tt.Check_inDate >= clientRoom.Check_inDate && tt.Check_inDate <= clientRoom.Check_outDate) || (tt.Check_outDate >= clientRoom.Check_inDate && tt.Check_outDate <= clientRoom.Check_outDate)))
                {

                    return 2;// BadRequest("you're not allowed to create record for room which in use during this dates");
                }
            }
            if (clientRoom.Check_outDate <= clientRoom.Check_inDate)
            {

                return 3; //BadRequest("Check out date can't be same or samller then check in date");
            }
            if (!ClientsExists(clientRoom.ClientID))
            {
                return 4;// BadRequest("Client doesnt exist");
            }
            if (!RoomsExists(clientRoom.RoomID))
            {
                return 5;// BadRequest("Room doesnt exist");
            }
            return 0;
        }
    }
}
