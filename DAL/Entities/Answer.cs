using DAL.Entities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Answer: BaseEntity
    {
        public int AnswerId { get; set; }
        public string AnswerString { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public bool CorrectAnswer { get; set; }
        public virtual ICollection<AnswerResult> AnswerResults { get; set; }

    }
}
