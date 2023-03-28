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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LayeringBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ClientsController));
        private readonly ISerClient<Client> _context;
        
        public ClientsController(ISerClient<Client> context)
        {
            _context = context;
        }
        [HttpPost("Auth")]
        public async Task<ActionResult<Client>> Auth(login l)
        {
            _log4net.Info("Auth Client is invoked");
            /*var ob = JsonConvert.DeserializeObject<JToken>(o.ToString());
            var ret= _context.Auth((string)ob["username"], (string)ob["password"]);*/
            var ret = _context.Auth(l);
            if (ret == null)
            {
                _log4net.Info("Invalid Auth Client is invoked");
                return BadRequest("Invalid Username or Password");
            }
            else
            {
                return Ok(ret);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            _log4net.Info("Get Client is invoked");
            var lc = _context.GetClients();
            return Ok(lc);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            _log4net.Info("Get Client by "+id+" is invoked");
            var client = _context.Get(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            _log4net.Info("Put Client is invoked");
            if (id != client.Cid)
            {
                return BadRequest();
            }

            //_context.Entry(client).State = EntityState.Modified;

            try
            {
                _context.Update(client);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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
                _log4net.Info("Email id exception is invoked");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _log4net.Info("Post Client is invoked");
            try
            {
                _context.Add(client);
            }
            catch (Exception e)
            {
                _log4net.Info("Exception Client is invoked");
                return BadRequest(e.Message);
            }
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Cid }, client);
        }

        private bool ClientExists(int id)
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
            //return _context.Clients.Any(e => e.Cid == id);
        }
    }
}
