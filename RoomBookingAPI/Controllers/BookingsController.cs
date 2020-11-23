using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBookingAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using RoomBookingAPI.Models;

namespace RoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly RoomBookingDBContext _context;
        private RoomsController rc;

        public BookingsController(RoomBookingDBContext context)
        {
            _context = context;
            rc = new RoomsController(context);
        }
        [HttpPut("GetBooking2")]
        public IEnumerable<Room> GetBooking2([FromBody] Booking booking)
        {
            return _context.Room.Where(e =>e.AdultsCapacity >= booking.NumberOfAdults && e.ChildrenCapacity >= booking.NumberOfChildren).OrderBy(e=>e.Price);
        }


        // GET: api/Bookings
        [HttpGet]
        public IEnumerable<Booking> GetBooking()
        {
            return _context.Booking;
        }

     

        [HttpGet("GetBookingbyRoomnumber/{id}")]
        public IActionResult GetBookingbyRoomnumber([FromRoute] int id)
        {

            var booking = _context.Booking
                 .FirstOrDefault(e => e.RoomId == id && e.StatusId ==2);
            if (booking == null)
            {
                return NotFound("The Status found.");
            }
            return Ok(booking);
        }

        public IEnumerable<Booking> GetBookingbyRoomId(int id)
        {
           return  _context.Booking
                 .Where(e => e.RoomId == id );



        }

        [HttpGet("Occupancy/{date}")]
        public IEnumerable<double> Occupancy([FromRoute] DateTime date)
        {
            IEnumerable<Room> rooms = getRooms();

            int roomcount = rooms.ToList().Count();
            double[] arr =new double[8];
            DateTime date2 = new DateTime();
            for (int i =0;i<=7;i++)
            {
                date2 = date;
                date.AddDays(i);
                // arr[i] = date.AddDays(i);
                arr[i] =((double) _context.Booking.Where( e => e.CheckInDate == date.AddDays(i) && e.StatusId == 1).Count()/roomcount)*100;
                
               
            }

            return arr;
        }
         IEnumerable<Room>  getRooms()
        {
            return _context.Room;
        }



        [HttpGet("GetBookingbycheckin/{date}")]
        public IEnumerable<Booking> GetBookingbycheckin([FromRoute] DateTime date)
        {
            return _context.Booking.Where(e => e.CheckInDate == date && e.StatusId == 1);

           
        }


        [HttpGet("GetBookingbycheckout/{date}")]
        public IEnumerable<Booking> GetBookingbycheckout([FromRoute] DateTime date)
        {


            return _context.Booking.Where(e => e.CheckOutDate == date && e.StatusId==2);


        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = await _context.Booking.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking([FromRoute] int id, [FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
    }
}