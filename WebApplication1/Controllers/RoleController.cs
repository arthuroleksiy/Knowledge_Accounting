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
    public class RoleController: Controller
    {
        public RoleController(IRoleService question)
        {
            this.RoleService = question;
        }

        IRoleService RoleService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<RoleModel>> Get()
        {
            return RoleService.GetAll().ToArray();

        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] RoleModel questionsModel)
        {

            await RoleService.AddAsync(questionsModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
            return Ok(questionsModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
        }
    }
}
