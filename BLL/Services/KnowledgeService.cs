using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class KnowledgeService : IKnowledgeService
    {

        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public IKnowledgeToKnowledgeModel KnowledgeToKnowledgeModel { get; }
        public IAnswerService AnswerService { get; set; }
        public IQuestionService QuestionService { get; set; }

        public KnowledgeService(IUnitOfWork UnitOfWork, IMapper Mapper, IKnowledgeToKnowledgeModel KnowledgeToKnowledgeModel, IQuestionService QuestionService, IAnswerService AnswerService)
        {
            this.UnitOfWork = UnitOfWork;
            this.Mapper = Mapper;
            this.KnowledgeToKnowledgeModel = KnowledgeToKnowledgeModel;


            this.AnswerService = AnswerService;
            this.QuestionService = QuestionService;
  
        }

        public async Task<bool> CheckIfCorrect(KnowledgesModel model)
        {
            if (model.KnowledgeId > 0 || String.IsNullOrEmpty(model.KnowledgeName))
                return false;

            return true;
        }

        public async Task AddAsync(KnowledgesModel model)
        {
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
            
            await UnitOfWork.KnowledgeRepository.AddAsync(new Knowledge {  KnowledgeName = knowledge.KnowledgeName, Questions = qr });
            await UnitOfWork.SaveAsync();
        }

        public async Task<bool> IsValidForUpdate(KnowledgesModel knowledge)
        {
            var result = await QuestionService.IsValidForUpdate(knowledge.Questions.ToList());
            if (!ContainsIdAsync(knowledge.KnowledgeId) || String.IsNullOrEmpty(knowledge.KnowledgeName) || !result)
                return false;

            return true;
        }

        public async Task<bool> IsValidToDelete(int modelId)
        {

            var knowledge = await UnitOfWork.KnowledgeRepository.GetByIdAsync(modelId);

            if (UnitOfWork.KnowledgeResultRepository.FindAll().Select(i => i.KnowledgeId).Contains(knowledge.KnowledgeId))
                return false;

            var questionsResult = UnitOfWork.QuestionResultRepository.FindAll();
            var questionIds = questionsResult.Select(i => i.QuestionId);
            var resultIds = questionsResult.Select(i => i.AnswerResult.AnswerId);
            foreach (var i in knowledge.Questions)
            {
                if (questionIds.Contains(i.QuestionId))
                    return false;

                foreach (var j in i.Answers)
                {
                    if (resultIds.Contains(j.AnswerId))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool ContainsIdAsync(int modelId)
        {
            return UnitOfWork.KnowledgeRepository.FindAll().Select(i => i.KnowledgeId).Contains(modelId);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            var knowledge = UnitOfWork.KnowledgeRepository.GetByIdAsync(modelId).Result;
            var questions = knowledge.Questions.ToList();
            foreach (var i in questions) {
                await UnitOfWork.QuestionRepository.DeleteAnswerByQuestionIdAsync(i.QuestionId);
            }

            await UnitOfWork.KnowledgeRepository.DeleteQuestionsByKnowledgeIdAsync(knowledge.KnowledgeId);

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

        public async Task UpdateAsync(KnowledgesModel model)
        {
            var knowledge = KnowledgeToKnowledgeModel.ToKnowledge(model);
            var questions = Mapper.Map<IEnumerable<QuestionsModel>, List<Question>>(model.Questions.ToList());
            foreach (var i in questions)
            {
                foreach (var j in i.Answers)
                {
                    await Task.Run(() => UnitOfWork.AnswersRepository.Update(j));
                }
                await Task.Run(() => UnitOfWork.QuestionRepository.Update(i));
            }

            UnitOfWork.KnowledgeRepository.Update(knowledge);
            await UnitOfWork.SaveAsync();

            /*
            return Task.Run(() => {
                UnitOfWork.KnowledgeRepository.Update(book);
                UnitOfWork.SaveAsync();
            });*/
        }
    }
}









