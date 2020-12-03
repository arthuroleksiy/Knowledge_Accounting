using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Knowledge:BaseEntity
    {
        public int KnowledgeId { get; set; }
        [Required(ErrorMessage = "Name was not input")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Incorrect name length")]
        public string KnowledgeName { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        //[ForeignKey("AllTestId")]
       // public int AllTestId { get; set; }
       // [NotMapped]
        //public virtual AllTest AllTest { get; set; }
    }
}
