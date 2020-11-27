using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserService userService { get; set; }
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }
        /*[HttpPost]
        //[ValidateAntiForgeryToken]
        [IgnoreAntiforgeryToken] 
        public async Task<ActionResult> LoginUser(LoginModel model)
        {
            bool result = await userService.Authenticate(model);
            if (result == true)
                return Ok();
            else
             return NotFound();

        }*/
    }
}
