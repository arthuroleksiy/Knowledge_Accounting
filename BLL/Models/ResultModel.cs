using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ResultModel
    {
        public string AnswerString { get; set; }
        public int QuestionId { get; set; }
        public QuestionsModel Question { get; set; }
        public bool CorrectAnswer { get; set; }
    }
}
