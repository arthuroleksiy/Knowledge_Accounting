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
    public class KnowledgeController : ControllerBase
    {
        public KnowledgeController(IKnowledgeService readerService)
        {
            this.KnowledgeService = readerService;
        }
        IKnowledgeService KnowledgeService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<KnowledgesModel>> Get()
        {
            return KnowledgeService.GetAll().ToArray();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AnswersModel>>> GetById(int id)
        {
            return new ObjectResult(await KnowledgeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] KnowledgesModel knowledgeModel)
        {

            await KnowledgeService.AddAsync(knowledgeModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
            return Ok(knowledgeModel);
            //return CreatedAtRoute("GetById", new { id = bookModel.Id }, bookModel);
        }

        [HttpPut]
        public async Task<ActionResult> Update(KnowledgesModel knowledgeModel)
        {

            await KnowledgeService.UpdateAsync(knowledgeModel);

            return NoContent();

        }

        //DELETE: /api/books/1
        [HttpDelete("{id}")]
        //[HttpDelete("/api/books/{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            await KnowledgeService.DeleteByIdAsync(id);
            return Ok(id);

        }
    }
}
