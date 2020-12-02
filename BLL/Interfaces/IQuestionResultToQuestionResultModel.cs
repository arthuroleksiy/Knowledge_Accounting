using BLL.Models;
using DAL.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IQuestionResultToQuestionResultModel
    {
        public IEnumerable<QuestionResultModel> ToQuestionModel(IEnumerable<QuestionResult> result);
    }
}
