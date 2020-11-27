using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class KnowledgeResultModel
    {
        public string KnowledgeName { get; set; }
        public DateTime Date { get; set; }
        public int Result { get; set; }
        public ICollection<QuestionResultModel> Questions { get; set; }
        //public int AllTestId { get; set; }
        //public AllTest AllTest { get; set; }
    }
}
