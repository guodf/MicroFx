using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroFx.Identity.Dtos.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroFx.Identity.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetAsync()
        {
            return await Task.FromResult(Ok(new UserDto()));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}
