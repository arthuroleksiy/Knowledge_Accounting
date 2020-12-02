using BLL.Models;
using DAL.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IKnowledgeResultToKnowledgeResultModel
    {
        IEnumerable<KnowledgeResultModel> ToKnowledgeModel(IEnumerable<KnowledgeResult> result);
        KnowledgeResult ToKnowledge(KnowledgeResultModel result, int role);
    }
}
