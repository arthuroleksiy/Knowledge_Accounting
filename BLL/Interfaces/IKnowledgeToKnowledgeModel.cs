using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IKnowledgeToKnowledgeModel
    {
        public IEnumerable<KnowledgesModel> ToKnowledgeModel(IEnumerable<Knowledge> result);
        public Knowledge ToKnowledge(KnowledgesModel result);
    }
}
