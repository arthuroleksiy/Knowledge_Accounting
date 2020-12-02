using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities.Results
{
    public class AnswerResult: BaseEntity
    {
        public int AnswerResultId { get; set; }
        //public string AnswerString { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        public int QuestionResultId { get; set; }
        [ForeignKey("QuestionResultId")]
        public virtual QuestionResult QuestionResult { get; set; }
        //public bool CorrectAnswer { get; set; }

    }
}
