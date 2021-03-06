﻿using AutoMapper;
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
    public class AnswerService : IAnswerService
    {
        public AnswerService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = Mapper;
            
        }

        private IUnitOfWork UnitOfWork { get; set; }
        public IMapper mapper { get; set; }

        public IEnumerable<AnswersModel> GetAll()
        {
            var result =  this.UnitOfWork.AnswersRepository.FindAll();
            return mapper.Map<IEnumerable<Answer>, List<AnswersModel>>(result);
        }

        public Task<AnswersModel> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                var task = this.UnitOfWork.AnswersRepository.GetByIdAsync(id);
                return mapper.Map<Answer, AnswersModel>(task.Result);
            });
        }

        public async Task<bool> CheckIfCorrect(List<AnswersModel> model)
        {
            foreach (var i in model) {
                if(i.AnswerId < 0 || String.IsNullOrEmpty(i.AnswerString))
                {
                    return false;
                }
            }

            return true;
        }
        public async Task<bool> IsValidForUpdate(List<AnswersModel> answers)
        {
            foreach (var i in answers)
            {
                if (String.IsNullOrEmpty(i.AnswerString) || !HasId(i.AnswerId))
                    return false;
            }

            return true;
        }

        public bool HasId(int modelId)
        {
            return UnitOfWork.AnswersRepository.FindAll().Select(i => i.AnswerId).Contains(modelId);
        }

        public Task AddAsync(AnswersModel model)
        {
            Answer book = mapper.Map<Answer>(model);

            return Task.Run(() => {
                UnitOfWork.AnswersRepository.AddAsync(book);
                UnitOfWork.SaveAsync();
            });
        }

        public Task UpdateAsync(AnswersModel model)
        {
            var book = mapper.Map<Answer>(model);

            return Task.Run(() => {
                UnitOfWork.AnswersRepository.Update(book);
                UnitOfWork.SaveAsync();
            });
        }

        public Task DeleteByIdAsync(int modelId)
        {
            return Task.Run(() =>
            {
                UnitOfWork.AnswersRepository.DeleteByIdAsync(modelId);
                UnitOfWork.SaveAsync();
            });
        }
    }
}
