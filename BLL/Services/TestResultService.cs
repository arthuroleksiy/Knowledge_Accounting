using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestResultService: ITestResultService
    {
        IUnitOfWork UnitOfWork { get; set; }

        public TestResultService(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        
        public async Task ProcessResult(KnowledgeResultModel result)
        {

            //var result = UnitOfWork.KnowledgeResultRepository.AddAsync(new );
        }

    }
}
