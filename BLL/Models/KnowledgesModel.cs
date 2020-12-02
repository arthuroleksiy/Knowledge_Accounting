using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class KnowledgesModel
    {
        public int KnowledgeId { get; set; }
        public string KnowledgeName { get; set; }
        public ICollection<QuestionsModel> Questions { get; set; }
        public int AllTestId { get; set; }
        //public AllTest AllTest { get; set; }
        //public ICollection<KnowledgeUser> KnowledgeUser { get; set; }
    }
}
