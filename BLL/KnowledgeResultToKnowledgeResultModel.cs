using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Entities.Results;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BLL
{
    public class KnowledgeResultToKnowledgeResultModel : IKnowledgeResultToKnowledgeResultModel
    {
        IMapper Mapper { get; }
        IQuestionToQuestionModel questionToQuestionModel { get; }
        IUnitOfWork unitOfWork { get;  } 
        public KnowledgeResultToKnowledgeResultModel(IMapper Mapper, IQuestionToQuestionModel questionToQuestionModel, IUnitOfWork unitOfWork)
        {
            this.Mapper = Mapper;
            this.questionToQuestionModel = questionToQuestionModel;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<KnowledgeResultModel> ToKnowledgeModel(IEnumerable<KnowledgeResult> result)
        {
            List<KnowledgeResultModel> knowledgeResultModel = new List<KnowledgeResultModel>();
            foreach (var i in result)
            {
                KnowledgeResultModel krm = new KnowledgeResultModel();
                krm.Id = i.KnowledgeResultId;
                krm.KnowledgeName = i.Knowledge.KnowledgeName;
                var res = i.QuestionResults.Where(i => i.KnowledgeResultId == krm.Id).Select(i => i);
                krm.Questions = Mapper.Map<IEnumerable<QuestionResult>,List<QuestionResultModel>>(res);
                krm.UserId = i.UserId;
                knowledgeResultModel.Add(krm);
            }
            return knowledgeResultModel.ToArray();
        }

        public KnowledgeResult ToKnowledge(KnowledgeResultModel result, int role)
        {
                KnowledgeResult krm = new KnowledgeResult();
                krm.KnowledgeResultId = result.Id;
                krm.KnowledgeId = unitOfWork.KnowledgeRepository.FindAll().Where(i => i.KnowledgeName == result.KnowledgeName).FirstOrDefault().KnowledgeId;
                krm.Result = CountResult(result);
                krm.QuestionResults = Mapper.Map<IEnumerable<QuestionResultModel>, List<QuestionResult>>(result.Questions);
                krm.Date = DateTime.Now;
                krm.UserId = role;
            return krm;
        }

        public int CountResult(KnowledgeResultModel result)
        {
            float mark = 0;
            foreach(var i in result.Questions)
            {
                if (i.Answer.CorrectAnswer == true)
                {
                    mark += 100 / result.Questions.Count;
                }
            }

            return (int)Math.Round(mark);
        }
    }
}
