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

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] KnowledgesModel knowledgeModel)
        {
            try
            {
                if (knowledgeModel == null)
                {
                    return BadRequest("Model object is null");
                }
                if(!ModelState.IsValid)
                {
                    return BadRequest("Wrong input");
                }
                await KnowledgeService.AddAsync(knowledgeModel);
                return CreatedAtRoute(new { name = knowledgeModel.KnowledgeName }, knowledgeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public async Task<ActionResult> Update(KnowledgesModel knowledgeModel)
        {
            try 
            {
                if (knowledgeModel == null)
                {
                    return BadRequest("Owner object is null");
                }

                if(!ModelState.IsValid)
                {
                    return BadRequest("Wrong input");
                }
                var isValid = await KnowledgeService.IsValidForUpdate(knowledgeModel);

                if (!isValid)
                {
                    return BadRequest("Wrong input");
                }

                await KnowledgeService.UpdateAsync(knowledgeModel);
                
                return NoContent();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var validTodelete = await KnowledgeService.IsValidToDelete(id);
                if (KnowledgeService.ContainsIdAsync(id) && validTodelete)
                {
                    await KnowledgeService.DeleteByIdAsync(id);
                    return Ok(id);
                }
                else
                {
                    return BadRequest("Value has not been found or has connections with others");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
