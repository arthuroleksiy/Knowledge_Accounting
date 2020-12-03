using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class AnswersModel
    {
        //[Required]
        public int AnswerId { get; set; }
        [Required]
        public string AnswerString { get; set; }
        public int QuestionId { get; set; }
        public QuestionsModel Question { get; set; }
        [Required]
        public bool CorrectAnswer { get; set; }
    }
}
