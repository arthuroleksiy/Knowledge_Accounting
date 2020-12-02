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
    public class AnswerResultService : IAnswerResultService
    {
        IUnitOfWork UnitOfWork { get; set; }
        private IMapper mapper { get; set; }
        public AnswerResultService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = mapper;
        }
        public IEnumerable<AnswerResultModel> GetAll()
        {

            var result = UnitOfWork.AnswersResultsReposistory.FindAll();
            return mapper.Map<IEnumerable<AnswerResult>, IEnumerable<AnswerResultModel>>(result);
        }
    }
}
