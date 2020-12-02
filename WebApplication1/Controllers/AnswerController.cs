using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase

    {
        public AnswerController(IAnswerService readerService)
        {
            this.AnswerService = readerService;
        }
        IAnswerService AnswerService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<AnswersModel>> Get()
        {
            try
            {
                var result = AnswerService.GetAll().ToArray();
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
                var result = await AnswerService.GetByIdAsync(id);

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

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AnswersModel answersModel)
        {

            try 
            {
                if (answersModel == null)
                {
                    return BadRequest("Answer object is null");
                }

                await AnswerService.AddAsync(answersModel); 
                return CreatedAtRoute("AnswerId", new { id = answersModel.AnswerId }, answersModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, AnswersModel answersModel)
        {
            try 
            {
                if (answersModel == null)
                {
                    return BadRequest("Answer object is null");
                }

                await AnswerService.UpdateAsync(answersModel);
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
                var result = await AnswerService.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                await AnswerService.DeleteByIdAsync(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }
    }

}




