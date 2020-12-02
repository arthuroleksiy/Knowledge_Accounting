using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class KnowledgeResultModel
    {
        public int Id { get; set; }
        public string KnowledgeName { get; set; }
        public DateTime Date { get; set; }
        public int TotalResult { get; set; }
        public ICollection<QuestionResultModel> Questions { get; set; }  
        public int UserId { get; set; }
        //public int AllTestId { get; set; }
        //public AllTest AllTest { get; set; }
    }
}
