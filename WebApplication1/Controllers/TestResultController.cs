using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController: Controller
    {
        /*
        public async Task<ActionResult> ProcessResult([FromBody] KnowledgeResultModel result)
        {

        }*/

        public TestResultController(ITestResultService question)
        {
            this.TestResultService = question;
        }

        ITestResultService TestResultService { get; }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<KnowledgeResultModel>> GetAllResults()
        {
            try
            {

                var result = TestResultService.GetAll().ToArray();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        
        [Authorize(Roles = Roles.User)]
        [HttpPost]
        public async Task<ActionResult> ProcessResult([FromBody] KnowledgeResultModel knowledgeResultModel)
        {
            try {

                if (knowledgeResultModel == null)
                {
                    return BadRequest("Model object is null");
                }

                await TestResultService.ProcessResult(knowledgeResultModel, Int32.Parse(User.Identity.Name));
                return CreatedAtRoute("Result processed", new { id = knowledgeResultModel.Id }, knowledgeResultModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [Authorize]
        [HttpPost("GetSpecificResults")]
        public ActionResult<IEnumerable<KnowledgeResultModel>> GetSpecificResults(SpecificResultModel specificResult)
        {
            try
            {
                var result = TestResultService.GetSpecificResults(specificResult).ToArray();
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
