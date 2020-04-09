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
using Shared.Interfaces.Models;
using Shared.Models.DomainModels;

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
        public IActionResult Post([FromBody] UserDTO user)
        {

            Console.WriteLine(user);
            var message = _provider.GetService<IMessage>();
            message.Command = Command.Create.ToString();
            message.Payload = JsonConvert.SerializeObject(user);

            var queue = _provider.GetService<IQueueRepo>();

            try
            {
                queue.AddMessage(message, "users");

                return Ok(new
                {
                    Message = "New User Created"
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown: {ex}");
                return StatusCode(500);
            }
                     
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
