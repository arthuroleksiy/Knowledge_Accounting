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
        //public Answer(int answerId, string answerString) => (AnswerId, AnswerString) = (answerId, answerString);
        //public int QuestionId { get; set; }
        // public virtual Question Question { get; set; }
        //public int ResultQuestionId { get; set; }
        //public virtual ResultQuestion ResultQuestion { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public bool CorrectAnswer { get; set; }

        //public  int CorrectQuestionId { get; set; }
        //public virtual Question CorrectQuestion { get; set; }
        //public  int UserQuestionId { get; set; }
        //public virtual Question UserQuestion { get; set; }
    }
}
