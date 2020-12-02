using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class AnswerResultModel
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public bool CorrectAnswer { get; set; }
        public string AnswerString { get; set; }
        // public  Answer Answer { get; set; }
        public int QuestionResultModelId { get; set; }



    }
}
