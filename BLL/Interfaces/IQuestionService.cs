using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IQuestionService: ICrud<QuestionsModel>
    {
        public Task<bool> IsValidForUpdate(List<QuestionsModel> knowledge);
        public bool HasId(int modelId);

    }
}
