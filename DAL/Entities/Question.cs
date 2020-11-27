using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Question: BaseEntity
    {
        public int QuestionId { get; set; }
        public string QuestionString { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        //public int CorrectAnswerId { get; set; }
        //public virtual Answer CorrectAnswer { get; set; }
        public int KnowledgeId { get; set; }
        public virtual Knowledge Knowledge { get; set; }
    }
}
