using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LayeringBookAPI.Models;
using LayeringBookAPI.Repository;

namespace LayeringBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibStaffsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LibStaffsController));
        private readonly IRepoLibStaff<LibStaff> _context;

        public LibStaffsController(IRepoLibStaff<LibStaff> context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibStaff>>> GetAll()
        {
            _log4net.Info("Get Staff is invoked");
            var lc = _context.GetStaff();
            return Ok(lc);
        }
        // GET: api/LibStaffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibStaff>> GetLibStaff(int id)
        {
            _log4net.Info("Get Staff by "+id+" is invoked");
            var libStaff = _context.Get(id);

            if (libStaff == null)
            {
                return NotFound();
            }

            return libStaff;
        }
        [HttpPost("Auth")]
        public async Task<ActionResult<LibStaff>> Auth(login l)
        {
            _log4net.Info("Auth Staff is invoked");
            /*var ob = JsonConvert.DeserializeObject<JToken>(o.ToString());
            var ret= _context.Auth((string)ob["username"], (string)ob["password"]);*/
            //var ret = _context.Auth(l);
            var c = _context.GetStaff();
            var result = (from i in c
                          where i.Userid == l.Userid && i.Password == l.Password
                          select i).SingleOrDefault();
            if (result == null)
            {
                return BadRequest("Invalid Username or Password");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
