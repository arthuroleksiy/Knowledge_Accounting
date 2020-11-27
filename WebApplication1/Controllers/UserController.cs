using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }
        
        IUserService UserService { get; }
        /*[HttpGet]
        public ActionResult<ApplicationUser> Get(string email)
        {
            return Ok(UserService.GetUserByEmail(email).Result);

        }*/


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await UserService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = UserService.GetAll();
            return Ok(users);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] UserModel userModel)
        {

            await UserService.CreateUser(userModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
            return Ok(userModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
        }

        [HttpPost("AddAdmin")]
        public async Task<ActionResult> AddAdmin([FromBody] UserModel userModel)
        {

            await UserService.CreateAdmin(userModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
            return Ok(userModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
        }
    }
}
