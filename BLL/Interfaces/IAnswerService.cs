using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAnswerService: ICrud<AnswersModel>
    {
        public Task<bool> CheckIfCorrect(List<AnswersModel> model);

        public Task<bool> IsValidForUpdate(List<AnswersModel> answers);

        public bool HasId(int modelId);


    }
}
