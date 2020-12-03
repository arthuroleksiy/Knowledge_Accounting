using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            try
            {
                var response = await UserService.Authenticate(model);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = UserService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] UserModel userModel)
        {

            try
            {
                if (userModel == null)
                {
                    return BadRequest("Model object is null");
                }
                await UserService.CreateUser(userModel); 
                return CreatedAtRoute("Username", new { id = userModel.UserName }, userModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("AddAdmin")]
        public async Task<ActionResult> AddAdmin([FromBody] UserModel userModel)
        {
            try
            {
                if (userModel == null)
                {
                    return BadRequest("Model object is null");
                }
                await UserService.CreateAdmin(userModel);
                return CreatedAtRoute("Admin name", new { id = userModel.UserName }, userModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
