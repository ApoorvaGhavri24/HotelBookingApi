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
    public class RoomAminitiesController : ControllerBase
    {
        private readonly RoomBookingDBContext _context;

        public RoomAminitiesController(RoomBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/RoomAminities
        [HttpGet]
        public IEnumerable<RoomAminity> GetRoomAminity()
        {
            return _context.RoomAminity;
        }

        // GET: api/RoomAminities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomAminity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomAminity = await _context.RoomAminity.FindAsync(id);

            if (roomAminity == null)
            {
                return NotFound();
            }

            return Ok(roomAminity);
        }

        // PUT: api/RoomAminities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomAminity([FromRoute] int id, [FromBody] RoomAminity roomAminity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomAminity.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomAminity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomAminityExists(id))
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

        // POST: api/RoomAminities
        [HttpPost]
        public async Task<IActionResult> PostRoomAminity([FromBody] RoomAminity roomAminity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RoomAminity.Add(roomAminity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomAminity", new { id = roomAminity.Id }, roomAminity);
        }

        // DELETE: api/RoomAminities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAminity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomAminity = await _context.RoomAminity.FindAsync(id);
            if (roomAminity == null)
            {
                return NotFound();
            }

            _context.RoomAminity.Remove(roomAminity);
            await _context.SaveChangesAsync();

            return Ok(roomAminity);
        }

        private bool RoomAminityExists(int id)
        {
            return _context.RoomAminity.Any(e => e.Id == id);
        }
    }
}