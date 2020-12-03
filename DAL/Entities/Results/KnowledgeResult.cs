using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities.Results
{
    public class KnowledgeResult: BaseEntity
    {
        public int KnowledgeResultId { get; set; }
        public int KnowledgeId { get; set; }
        public virtual Knowledge Knowledge { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public int Result { get; set; }
        public virtual ICollection<QuestionResult> QuestionResults { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
