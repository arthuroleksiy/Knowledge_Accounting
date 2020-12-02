using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionResutController: ControllerBase
    {
        public QuestionResutController(IQuestionResultService question)
        {
            this.QuestionResultService = question;
        }

        IQuestionResultService QuestionResultService { get; }
        [HttpGet]
        public ActionResult<IEnumerable<QuestionResultModel>> GetAllResuts()
        {
            try {
                var result = QuestionResultService.GetAll().ToArray();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
