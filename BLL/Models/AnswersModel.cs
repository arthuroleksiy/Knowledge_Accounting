using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class AnswersModel
    {
        public int AnswerId { get; set; }
        public string AnswerString { get; set; }
        public int QuestionId { get; set; }
        public QuestionsModel Question { get; set; }
        public bool CorrectAnswer { get; set; }
    }
}
