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
    public class KnowledgeController : Controller
    {
        public KnowledgeController(IKnowledgeService readerService)
        {
            this.KnowledgeService = readerService;
        }
        IKnowledgeService KnowledgeService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<KnowledgesModel>> Get()
        {
            try
            {
                var result =  KnowledgeService.GetAll().ToArray();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AnswersModel>>> GetById(int id)
        {
            try 
            {
                var result = await KnowledgeService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] KnowledgesModel knowledgeModel)
        {
            try
            {
                if (knowledgeModel == null)
                {
                    return BadRequest("Model object is null");
                }

                await KnowledgeService.AddAsync(knowledgeModel);
                //return CreatedAtRoute("Knowledge added", new {   = knowledgeResultModel.Id }, knowledgeResultModel);
                return CreatedAtRoute(new { name = knowledgeModel.KnowledgeName }, knowledgeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(KnowledgesModel knowledgeModel)
        {
            try 
            {
                if (knowledgeModel == null)
                {

                    return BadRequest("Owner object is null");
                }
                await KnowledgeService.UpdateAsync(knowledgeModel);
                return NoContent();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await KnowledgeService.DeleteByIdAsync(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
