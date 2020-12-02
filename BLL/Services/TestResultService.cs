using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities.Results;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestResultService: ITestResultService
    {
        IUnitOfWork UnitOfWork { get; set; }
        private IMapper mapper { get; set; } 
        IKnowledgeResultToKnowledgeResultModel KnowledgeResultToKnowledgeResultModel { get; set; }
        public TestResultService(IUnitOfWork UnitOfWork, IMapper mapper, IKnowledgeResultToKnowledgeResultModel KnowledgeResultToKnowledgeResultModel)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = mapper;
            this.KnowledgeResultToKnowledgeResultModel = KnowledgeResultToKnowledgeResultModel;
        }
        
        public async Task ProcessResult(KnowledgeResultModel result, int userId)
        {
            var knowledge = KnowledgeResultToKnowledgeResultModel.ToKnowledge(result, userId);

            List<QuestionResult> qr = new List<QuestionResult>();

            foreach(var i in knowledge.QuestionResults)
            {
                qr.Add(new QuestionResult { QuestionId = i.QuestionId, AnswerResult = new AnswerResult { AnswerId = i.AnswerResult.AnswerId } });
            }
            await UnitOfWork.KnowledgeResultRepository.AddAsync(new KnowledgeResult {  KnowledgeId = knowledge.KnowledgeId, UserId = knowledge.UserId, Date = knowledge.Date, Result = knowledge.Result, QuestionResults = qr   });

            await UnitOfWork.SaveAsync();
           
        }

        public IEnumerable<KnowledgeResultModel> GetAll()
        {
            var result = UnitOfWork.TestResultsRepository.FindAll();
            return mapper.Map<IEnumerable<KnowledgeResult>, IEnumerable<KnowledgeResultModel>>(result);
        }

        public IEnumerable<KnowledgeResultModel> GetSpecificResults(SpecificResultModel specificResult)
        {
            var result = UnitOfWork.TestResultsRepository.FindByDateAndPoint(specificResult.MinPoint, specificResult.MaxPoint, specificResult.StartDate, specificResult.EndDate);
            List<KnowledgeResultModel> output = KnowledgeResultToKnowledgeResultModel.ToKnowledgeModel(result).ToList();
            var a2 = UnitOfWork.KnowledgeResultRepository.FindAll();

            for (int i = 0; i < output.Count; i++)
            {
                output[i].UserId =  a2.Where(j => j.KnowledgeResultId == output[i].Id).Select(j => j.UserId).FirstOrDefault();
            }

            return output;
        }

    }
}
