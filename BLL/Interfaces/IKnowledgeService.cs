using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IKnowledgeService: ICrud<KnowledgesModel>
    {
        public IAnswerService AnswerService { get; set; }
        public IQuestionService QuestionService { get; set; }
        public Task<bool> CheckIfCorrect(KnowledgesModel model);
        public Task<bool> IsValidForUpdate(KnowledgesModel knowledge);
        public bool ContainsIdAsync(int modelId);
        public Task<bool> IsValidToDelete(int modelId);

    }
}
