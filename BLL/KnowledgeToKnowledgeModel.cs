using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class KnowledgeToKnowledgeModel : IKnowledgeToKnowledgeModel
    {


        IMapper Mapper { get; }
        IQuestionToQuestionModel questionToQuestionModel { get; }
        public KnowledgeToKnowledgeModel(IMapper Mapper, IQuestionToQuestionModel questionToQuestionModel)
        {
            this.Mapper = Mapper;
            this.questionToQuestionModel = questionToQuestionModel;
        }

        public IEnumerable<KnowledgesModel> ToKnowledgeModel(IEnumerable<Knowledge> result)
        {
            List<KnowledgesModel> knowledgesModels = new List<KnowledgesModel>();
            foreach (var i in result)
            {
                KnowledgesModel knowledgesModel = new KnowledgesModel();
                knowledgesModel.KnowledgeId = i.KnowledgeId;
                knowledgesModel.KnowledgeName = i.KnowledgeName;
                var res = i.Questions.Where(j => j.KnowledgeId == i.KnowledgeId).Select(j => j);
                knowledgesModel.Questions = questionToQuestionModel.ToQuestionModel(res).ToList();

                knowledgesModels.Add(knowledgesModel);
            }
            return knowledgesModels.ToArray();
        }


        public Knowledge ToKnowledge(KnowledgesModel result)
        {
            Knowledge krm = new Knowledge();
            krm.KnowledgeId = result.KnowledgeId;
            krm.KnowledgeName = result.KnowledgeName;
            krm.Questions = Mapper.Map<IEnumerable<QuestionsModel>, List<Question>>(result.Questions);
            krm.AllTestId = result.AllTestId;
            return krm;
        }
    }
}
