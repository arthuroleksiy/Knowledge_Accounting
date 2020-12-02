using BLL.Models;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IQuestionResultService
    {
        IEnumerable<QuestionResultModel> GetAll();
    }
}
