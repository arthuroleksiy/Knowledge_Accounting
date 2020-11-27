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
            return QuestionService.GetAll().ToArray();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<QuestionsModel>>> GetById(int id)
        {
            return new ObjectResult(await QuestionService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] QuestionsModel questionsModel)
        {

            await QuestionService.AddAsync(questionsModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
            return Ok(questionsModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
        }

        [HttpPut]
        public async Task<ActionResult> Update(QuestionsModel questionsModel)
        {

            await QuestionService.UpdateAsync(questionsModel);

            return NoContent();

        }

        //DELETE: /api/books/1
        [HttpDelete("{id}")]
        //[HttpDelete("/api/books/{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            await QuestionService.DeleteByIdAsync(id);
            return Ok(id);

        }
    }
}
