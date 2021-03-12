using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookExchangerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<BookExchanger.Service.Dto.User.Get.ResponseDto> Get(string id)
        {
            BookExchanger.Service.IUserService userService = new BookExchanger.Service.Services.UserService();
            var user = await userService.GetAsync(id, true);

            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<string> Post()
        {
            BookExchanger.Service.IUserService userService = new BookExchanger.Service.Services.UserService();
            var userId = await userService.CreateAsync(new BookExchanger.Service.Dto.User.Create.RequestDto()
            {
                Address = "ADDRESS",
                City = "CITY",
                Country = "COUNTRY",
                Degree = "DEGREE",
                Description = "DESCRIPTION",
                District = "DISTRICT",
                NickName = "NICKNAME",
                Password = "PASSWORD",
                Phone = "PHONE",
                Province = "PROVINCE",
                UserName = "FREDTU"
            });

            return userId;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}