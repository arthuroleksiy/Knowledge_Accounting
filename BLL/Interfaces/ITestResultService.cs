using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITestResultService
    {

        Task ProcessResult(KnowledgeResultModel result, int userId);
        IEnumerable<KnowledgeResultModel> GetAll();
        IEnumerable<KnowledgeResultModel> GetSpecificResults(SpecificResultModel specificResult);

    }
}
