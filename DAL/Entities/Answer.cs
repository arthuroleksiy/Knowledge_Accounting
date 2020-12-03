using DAL.Entities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Answer: BaseEntity
    {
        public int AnswerId { get; set; }

        [Required(ErrorMessage = "Answer was not input")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Incorrect answer length")]
        public string AnswerString { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [Required]
        public bool CorrectAnswer { get; set; }
        public virtual ICollection<AnswerResult> AnswerResults { get; set; }

    }
}
