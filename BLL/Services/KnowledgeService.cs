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
        public Task AddAsync(KnowledgesModel model)
        {
            Knowledge knowledge = Mapper.Map<Knowledge>(model);

            return Task.Run(() => {
                UnitOfWork.KnowledgeRepository.AddAsync(knowledge);
                UnitOfWork.SaveAsync();
            });
        }

        public Task DeleteByIdAsync(int modelId)
        {
            return Task.Run(() =>
            {
                UnitOfWork.KnowledgeRepository.DeleteByIdAsync(modelId);
                UnitOfWork.SaveAsync();
            });
        }

        public IEnumerable<KnowledgesModel> GetAll()
        {
            var result = UnitOfWork.KnowledgeRepository.FindAll();
            return KnowledgeToKnowledgeModel.ToKnowledgeModel(result);
            //return Mapper.Map<IEnumerable<Knowledge>, List<KnowledgesModel>>(result);
        }

        public Task<KnowledgesModel> GetByIdAsync(int id)
        {

            IEnumerable<Knowledge> list = new List<Knowledge>();
            return Task.Run(() =>
            {
                var task = UnitOfWork.KnowledgeRepository.GetByIdAsync(id);
                return Mapper.Map<Knowledge, KnowledgesModel>(task.Result);

                //return KnowledgeToKnowledgeModel.ToKnowledgeModel(task.Result);
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









