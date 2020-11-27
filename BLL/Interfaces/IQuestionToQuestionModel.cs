using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IQuestionToQuestionModel
    {
        public IEnumerable<QuestionsModel> ToQuestionModel(IEnumerable<Question> result);
    }
}
