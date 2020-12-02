using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController: ControllerBase
    {
        public RoleController(IRoleService question)
        {
            this.RoleService = question;
        }

        IRoleService RoleService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<RoleModel>> Get()
        {
            try
            {
                var result = RoleService.GetAll().ToArray();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] RoleModel questionsModel)
        {
            try {
                if (questionsModel == null)
                {
                    return BadRequest("Question object is null");
                }
                await RoleService.AddAsync(questionsModel);
                return Ok(questionsModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
