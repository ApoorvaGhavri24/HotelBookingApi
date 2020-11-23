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
    public class AmenitiesController : ControllerBase
    {
        private readonly RoomBookingDBContext _context;

        public AmenitiesController(RoomBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/Amenities
        [HttpGet]
        public IActionResult GetAmenity()
        {
            IEnumerable<Amenity> amenitys = _context.Amenity.ToList();
            return Ok(amenitys);
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public  IActionResult GetAmenity([FromRoute] int id)
        {
            Amenity amenity = _context.Amenity
                 .FirstOrDefault(e => e.Id == id);
           
            if (amenity == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        [HttpPut("{id}")]
        public IActionResult PutAmenity([FromRoute] int id, [FromBody] Amenity amenity)
        {

            if (amenity == null)
            {
                return BadRequest("Employee is null.");
            }
            Amenity amenitytoupdate = _context.Amenity
                  .FirstOrDefault(e => e.Id == id);

            if (amenitytoupdate == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            amenitytoupdate.Label = amenity.Label;
            amenitytoupdate.Amenityorder = amenity.Amenityorder;
            amenitytoupdate.IsActive = amenity.IsActive;
     
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/Amenities
        [HttpPost]
        public async Task<IActionResult> PostAmenity([FromBody] Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Amenity.Add(amenity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var amenity = await _context.Amenity.FindAsync(id);
            if (amenity == null)
            {
                return NotFound();
            }

            _context.Amenity.Remove(amenity);
            await _context.SaveChangesAsync();

            return Ok(amenity);
        }

        private bool AmenityExists(int id)
        {
            return _context.Amenity.Any(e => e.Id == id);
        }
    }
}