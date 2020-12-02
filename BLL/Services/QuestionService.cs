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
            return this.QuestionToQuestionModel.ToQuestionModel(result);
            
        }

        public Task<QuestionsModel> GetByIdAsync(int id)
        {
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
