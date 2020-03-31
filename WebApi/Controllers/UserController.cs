using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Interfaces.Messages;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces.Infrastructure;
using Shared.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IServiceProvider _provider;

        public UserController(IServiceProvider provider)
        {
            _provider = provider;
        }


        //// GET: api/User
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "LendersAPI", "Running" };
        //}

        // GET: api/User/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/User
        [HttpPost]
        [Authorize]
        public void Post([FromBody] dynamic user)
        {
            Console.WriteLine(user);
        }

        //// PUT: api/User/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
