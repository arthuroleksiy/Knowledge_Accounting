using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities.Results
{
    public class KnowledgeResult: BaseEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KnowledgeResultId { get; set; }
        public string KnowledgeName { get; set; }
        public DateTime Date { get; set; }
        public int Result { get; set; }
        public virtual ICollection<QuestionResult> QuestionResults { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
