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
    public class QuestionService : IQuestionService
    {

        private IUnitOfWork UnitOfWork { get; set; }
        private IMapper mapper { get; set; }
        private IQuestionToQuestionModel QuestionToQuestionModel { get; set; }
        public  QuestionService(IUnitOfWork UnitOfWork, IMapper Mapper, IQuestionToQuestionModel QuestionToQuestionModel)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = Mapper;
            this.QuestionToQuestionModel = QuestionToQuestionModel;
        }
        public Task AddAsync(QuestionsModel model)
        {
            Question question = mapper.Map<Question>(model);

            return Task.Run(() => {
                UnitOfWork.QuestionRepository.AddAsync(question);
                UnitOfWork.SaveAsync();
            });
        }

        public Task DeleteByIdAsync(int modelId)
        {
            return Task.Run(() =>
            {
                UnitOfWork.QuestionRepository.DeleteByIdAsync(modelId);
                UnitOfWork.SaveAsync();
            });
        }

        public IEnumerable<QuestionsModel> GetAll()
        {
            var result = UnitOfWork.QuestionRepository.FindAll();
            //return mapper.Map<IEnumerable<Question>, List<QuestionsModel>>(result);

            /*List<QuestionsModel> questionsModels = new List<QuestionsModel>();
            foreach (var i in result)
            {
                QuestionsModel questionsModel = new QuestionsModel();
                questionsModel.QuestionId = i.QuestionId;
                questionsModel.QuestionString = i.QuestionString;
                questionsModel.KnowledgeId = i.KnowledgeId;
                var res = i.Answers.Where(j => j.QuestionId == i.QuestionId).Select(j => j);
                questionsModel.Answers = mapper.Map<IEnumerable<Answer>, List<AnswersModel>>(res);

                questionsModels.Add(questionsModel);
            }*/
            return this.QuestionToQuestionModel.ToQuestionModel(result);
            
        }

        public Task<QuestionsModel> GetByIdAsync(int id)
        {
            //IEnumerable<Question> list = new List<Knowledge>();
            return Task.Run(() =>
            {
                var task = UnitOfWork.QuestionRepository.GetByIdAsync(id);
                return mapper.Map<Question, QuestionsModel>(task.Result);

            });
        }

        public Task UpdateAsync(QuestionsModel model)
        {
            var book = mapper.Map<QuestionsModel, Question>(model);
            return Task.Run(() => {
                UnitOfWork.QuestionRepository.Update(book);
                UnitOfWork.SaveAsync();
            });
        }
    }
}
