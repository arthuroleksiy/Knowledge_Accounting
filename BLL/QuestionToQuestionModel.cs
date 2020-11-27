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
    public class QuestionToQuestionModel: IQuestionToQuestionModel
    {
        IMapper Mapper { get; }
        public QuestionToQuestionModel(IMapper Mapper)
        {
            this.Mapper = Mapper;
        }
        public IEnumerable<QuestionsModel> ToQuestionModel(IEnumerable<Question> result)
        {
            List<QuestionsModel> questionsModels = new List<QuestionsModel>();
            foreach (var i in result)
            {
                QuestionsModel questionsModel = new QuestionsModel();
                questionsModel.QuestionId = i.QuestionId;
                questionsModel.QuestionString = i.QuestionString;
                questionsModel.KnowledgeId = i.KnowledgeId;
                var res = i.Answers.Where(j => j.QuestionId == i.QuestionId).Select(j => j);
                questionsModel.Answers = this.Mapper.Map<IEnumerable<Answer>, List<AnswersModel>>(res);

                questionsModels.Add(questionsModel);
            }
            return questionsModels.ToArray();
        }
    }
}
