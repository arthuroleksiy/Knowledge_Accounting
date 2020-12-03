using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class AnswerResultModel
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        [Required]
        public bool CorrectAnswer { get; set; }
        [Required]
        public string AnswerString { get; set; }
        public int QuestionResultModelId { get; set; }



    }
}
