using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LayeringBookAPI.Models;
using LayeringBookAPI.Services;
using System.Security.Cryptography;

namespace LayeringBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingsController));
        private readonly ISerBooking<Booking> _context;

        public BookingsController(ISerBooking<Booking> context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            _log4net.Info("Get Bookings is invoked");
            var bk = _context.GetBookings();
            return Ok(bk);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> Get(int id)
        {
            _log4net.Info("Get Booking is invoked");
            var booking = _context.Get(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            _log4net.Info("Put Booking is invoked");
            if (id != booking.Bkid)
            {
                return BadRequest();
            }           

            try
            {
                _context.Update(booking);
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }
        [HttpPost("Client_Orders")]
        public async Task<ActionResult<Booking>> Client_Orders(Booking booking)
        {
            _log4net.Info("Client_Orders Booking is invoked");
            var order = _context.Orders(booking);
            if (order == null)
            {
                return NotFound();
            }
            return order;
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("Get", new { id = booking.Bkid }, booking);
        }
        [HttpPost("Paid_Orders")]
        public async Task<ActionResult<List<Booking>>> Paid_Orders(Client cl)
        {
            _log4net.Info("Paid_Orders Booking is invoked");
            var order = _context.POrders(cl);
            if (order == null)
            {
                return NotFound();
            }
            return order;
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("Get", new { id = booking.Bkid }, booking);
        }
        [HttpPost("Unpaid_Orders")]
        public async Task<ActionResult<List<Booking>>> Unpaid_Orders(Client cl)
        {
            _log4net.Info("Unpaid_Orders Booking is invoked");
            var order = _context.UPOrders(cl);
            if (order == null)
            {
                return NotFound();
            }
            return order;
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("Get", new { id = booking.Bkid }, booking);
        }
        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _log4net.Info("Post Booking is invoked");
            try
            {
                _context.Add(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = booking.Bkid }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            _log4net.Info("Delete Booking is invoked");
            var booking = _context.Get(id);
            if (booking == null)
            {
                return NotFound();
            }
            try
            {
                _context.Delete(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            var x = _context.Get(id);
            if (x == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            //return _context.Get(id);
        }
    }
}
