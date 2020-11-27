using DAL.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class QuestionResultModel
    {
        public string QuestionString { get; set; }
        public AnswerResultModel Answer { get; set; }
    }
}
