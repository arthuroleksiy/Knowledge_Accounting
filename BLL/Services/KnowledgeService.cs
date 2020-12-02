using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class KnowledgeService : IKnowledgeService
    {

        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public IKnowledgeToKnowledgeModel KnowledgeToKnowledgeModel { get; }
        public KnowledgeService(IUnitOfWork UnitOfWork, IMapper Mapper, IKnowledgeToKnowledgeModel KnowledgeToKnowledgeModel)
        {
            this.UnitOfWork = UnitOfWork;
            this.Mapper = Mapper;
            this.KnowledgeToKnowledgeModel = KnowledgeToKnowledgeModel;
        }
        public async Task AddAsync(KnowledgesModel model)
        {
            ///Knowledge knowledge = Mapper.Map<Knowledge>(model);
            /*
             *var knowledge = KnowledgeResultToKnowledgeResultModel.ToKnowledge(result, userId);

            List<QuestionResult> qr = new List<QuestionResult>();

            foreach(var i in knowledge.QuestionResults)
            {
                qr.Add(new QuestionResult { QuestionId = i.QuestionId, AnswerResult = new AnswerResult { AnswerId = i.AnswerResult.AnswerId } });
            }
            await UnitOfWork.KnowledgeResultRepository.AddAsync(new KnowledgeResult {  KnowledgeId = knowledge.KnowledgeId, UserId = knowledge.UserId, Date = knowledge.Date, Result = knowledge.Result, QuestionResults = qr   });

            await UnitOfWork.SaveAsync();
            */

            var knowledge = KnowledgeToKnowledgeModel.ToKnowledge(model);

            List<Question> qr = new List<Question>();
            List<Answer> ar;
            foreach (var i in knowledge.Questions)
            {

                ar = new List<Answer>();
                foreach(var j in i.Answers)
                {
                    ar.Add(new Answer { AnswerString = j.AnswerString, CorrectAnswer = j.CorrectAnswer  });
                }

                qr.Add(new Question { QuestionString = i.QuestionString, Answers = ar  });

            }
                await UnitOfWork.KnowledgeRepository.AddAsync(new Knowledge { KnowledgeName = knowledge.KnowledgeName, Questions = qr, AllTestId = 1 });
                await UnitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
             await UnitOfWork.KnowledgeRepository.DeleteByIdAsync(modelId);
             await UnitOfWork.SaveAsync();
           
        }

        public IEnumerable<KnowledgesModel> GetAll()
        {
            var result = UnitOfWork.KnowledgeRepository.FindAll();
            return KnowledgeToKnowledgeModel.ToKnowledgeModel(result);
        }

        public Task<KnowledgesModel> GetByIdAsync(int id)
        {

            IEnumerable<Knowledge> list = new List<Knowledge>();
            return Task.Run(() =>
            {
                var task = UnitOfWork.KnowledgeRepository.GetByIdAsync(id);
                return Mapper.Map<Knowledge, KnowledgesModel>(task.Result);
            });

        }

        public Task UpdateAsync(KnowledgesModel model)
        {
            var book = Mapper.Map<KnowledgesModel, Knowledge>(model);
            return Task.Run(() => {
                UnitOfWork.KnowledgeRepository.Update(book);
                UnitOfWork.SaveAsync();
            });
        }
    }
}









