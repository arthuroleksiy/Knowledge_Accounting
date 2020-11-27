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
            return AnswerService.GetAll().ToArray();
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AnswersModel>>> GetById(int id)
        {
            return new ObjectResult(await AnswerService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AnswersModel answersModel)
        {


            await AnswerService.AddAsync(answersModel);
            return Ok(answersModel);
        }

        [HttpPut]
        public async Task<ActionResult> Update(AnswersModel answersModel)
        {

            await AnswerService.UpdateAsync(answersModel);

            return NoContent();

        }

        //DELETE: /api/books/1
        [HttpDelete("{id}")]
        //[HttpDelete("/api/books/{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            await AnswerService.DeleteByIdAsync(id);
            return Ok(id);

        }
    }

}




