using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities.Results;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class QuestionResultService : IQuestionResultService
    {
        IUnitOfWork UnitOfWork { get; set; }
        private IMapper mapper { get; set; }
        public QuestionResultService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = mapper;
        }
        public IEnumerable<QuestionResultModel> GetAll()
        {

            var result = UnitOfWork.QuestionResultRepository.FindAll();
            return mapper.Map<IEnumerable<QuestionResult>, IEnumerable<QuestionResultModel>>(result);
        }
    }
}
