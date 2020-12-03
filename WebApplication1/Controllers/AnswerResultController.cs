using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerResultController: ControllerBase
    {

        public AnswerResultController(IAnswerResultService question)
        {
            this.AnswerResultsService = question;
        }

        IAnswerResultService AnswerResultsService { get; }
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<AnswerResultModel>> GetAllResuts()
        {
            try
            {
                return AnswerResultsService.GetAll().ToArray();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
