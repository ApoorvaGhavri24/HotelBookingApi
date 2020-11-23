using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBookingAPI.Models;

namespace RoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly RoomBookingDBContext _context;

        public RoomsController(RoomBookingDBContext context)
        {
            _context = context;
        }


        // GET: api/Rooms
        [HttpGet]
        public IActionResult GetRoom()

        {
            IEnumerable<Room> rooms = _context.Room.ToList();
            return Ok(rooms);
           // return _context.Room;
        }

        public Room GetRoombynumber(int roomnumber)
        {
            Room room = _context.Room
                  .FirstOrDefault(e => e.RoomNumber == roomnumber);
            return room;
        }
        public Room GetRoombyId(int id)
        {
            Room room = _context.Room
                  .FirstOrDefault(e => e.Id == id);
            return room;
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public  IActionResult GetRoom([FromRoute] int id)
        {
            Room room= _context.Room
                  .FirstOrDefault(e => e.Id == id);
            //if (!ModelState.IsValid)

            //{
            //    return BadRequest(ModelState);
            //}

            //var room = await _context.Room.FindAsync(id);

            //if (room == null)
            //{
            //    return NotFound();
            //}

            //return Ok(room);
            if (room == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            return Ok(room);
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public IActionResult PutRoom([FromRoute] int id, [FromBody] Room room)
        {

            if (room == null)
            {
                return BadRequest("Employee is null.");
            }
            Room roomToupdate = _context.Room
                  .FirstOrDefault(e => e.Id == id);

            if (roomToupdate == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            roomToupdate.RoomNumber = room.RoomNumber;
            roomToupdate.AdultsCapacity = room.AdultsCapacity;
            roomToupdate.ChildrenCapacity = room.ChildrenCapacity;
            roomToupdate.Price = room.Price;
            _context.SaveChanges();
            return NoContent();

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != room.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(room).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!RoomExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<IActionResult> PostRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!RoomExistWithRoomNumber(room.RoomNumber))
            {
                _context.Room.Add(room);
                await _context.SaveChangesAsync();
            }

          

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return Ok(room);
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
        private bool RoomExistWithRoomNumber(int roomnumber)
        {
            return _context.Room.Any(e => e.RoomNumber == roomnumber);
        }


        [HttpGet("GetRoombyroomnumber/{rno}")]
        public IActionResult GetRoombyroomnumber([FromRoute] int rno)
        {
            Room room = _context.Room
                  .FirstOrDefault(e => e.RoomNumber == rno);
            //if (!ModelState.IsValid)

            //{
            //    return BadRequest(ModelState);
            //}

            //var room = await _context.Room.FindAsync(id);

            //if (room == null)
            //{
            //    return NotFound();
            //}

            //return Ok(room);
            if (room == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            return Ok(room);
        }

    }
}