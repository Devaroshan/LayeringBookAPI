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
using Library_MVC_API.Models;

namespace LayeringBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BooksController));
        private readonly ISerBook<Book> _context;

        public BooksController(ISerBook<Book> context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            _log4net.Info("Get Book is invoked");
            var lb = _context.GetBooks();
            return Ok(lb);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            _log4net.Info("Get Book by "+id+" is invoked");
            var book = _context.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        [HttpPost("Fav")]
        public async Task<ActionResult<IEnumerable<Book>>> Fav(Fav b)
        {
            _log4net.Info("Get Favourite Book is invoked");
            var lb = _context.Fav(b);
            return Ok(lb);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(string id, Book book)
        {
            _log4net.Info("Put Book is invoked");
            if (id != book.Bid)
            {
                return BadRequest();
            }

            //_context.Entry(book).State = EntityState.Modified;

            try
            {
                _context.Update(book);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch(Exception e) 
            {
                _log4net.Info("Bad request of Put Book is invoked");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _log4net.Info("Post Book is invoked");
            try
            {
                _context.Add(book);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.Bid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                _log4net.Info("Bad request of Post Book is invoked");
                return BadRequest(e.Message);
            }
            return CreatedAtAction("GetBook", new { id = book.Bid }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            _log4net.Info("Delete Book is invoked");
            var book = _context.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            try
            {
                _context.Delete(book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        private bool BookExists(string id)
        {
            var b = _context.Get(id);
            if (b == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            //return _context.Books.Any(e => e.Bid == id);
        }
    }
}
