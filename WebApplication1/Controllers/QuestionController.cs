using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        public QuestionController(IQuestionService question)
        {
            this.QuestionService = question;
        }
        IQuestionService QuestionService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<QuestionsModel>> Get()
        {
            try
            {
                var result =  QuestionService.GetAll().ToArray();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<QuestionsModel>>> GetById(int id)
        {
            try
            {
                
                var result =  await QuestionService.GetByIdAsync(id);

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
        public async Task<ActionResult> Add([FromBody] QuestionsModel questionsModel)
        {
            try
            {
                await QuestionService.AddAsync(questionsModel);
                return Ok(questionsModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(QuestionsModel questionsModel)
        {
            try
            {
                if (questionsModel == null)
                {
                    return BadRequest("Question object is null");
                }
                await QuestionService.UpdateAsync(questionsModel);

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
                await QuestionService.DeleteByIdAsync(id);
                return Ok(id);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "Internal server error"); 
            }

        }
    }
}
