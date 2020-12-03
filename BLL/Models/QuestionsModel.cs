using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class QuestionsModel
    {
        public int QuestionId { get; set; }
        [Required]
        public string QuestionString { get; set; }
        public ICollection<AnswersModel> Answers { get; set; }
        //public AnswerM CorrectAnswer { get; set; }
        public int KnowledgeId { get; set; }
        public KnowledgesModel Knowledge { get; set; }
    }
}
