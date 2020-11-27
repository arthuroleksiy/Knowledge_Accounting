using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.Results
{
    public class AnswerResult: BaseEntity
    {
        public int AnswerResultId { get; set; }
        public string AnswerString { get; set; }
        public int QuestionResultId { get; set; }
        public virtual QuestionResult QuestionResult { get; set; }
        public bool CorrectAnswer { get; set; }

    }
}
