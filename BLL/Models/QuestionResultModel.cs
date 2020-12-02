using DAL.Entities;
using DAL.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class QuestionResultModel
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public int Result { get; set; }
        public AnswerResultModel Answer { get; set; }
        public int QuestionId { get; set; }
        public int KnowledgeResultId { get; set; }
    }
}
