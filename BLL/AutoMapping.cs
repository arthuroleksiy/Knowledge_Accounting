using AutoMapper;
using BLL.Models;
using DAL.Entities;
using DAL.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {
            CreateMap<Answer, AnswersModel>()
                .ForMember(p => p.AnswerId, c => c.MapFrom(ca => ca.AnswerId))
                .ForMember(am => am.AnswerString, a => a.MapFrom(ca => ca.AnswerString))
                .ForMember(am => am.CorrectAnswer, a => a.MapFrom(ca => ca.CorrectAnswer))
                .ForMember(am => am.QuestionId, a => a.MapFrom(ca=> ca.QuestionId))
                .ForMember(am => am.Question, a => a.Ignore())
                .ReverseMap();


            
            CreateMap<Question, QuestionsModel>()
                .ForMember(p => p.QuestionId, c => c.MapFrom(ca => ca.QuestionId))
                .ForMember(am => am.QuestionString, a => a.MapFrom(ca => ca.QuestionString))
                .ForMember(am => am.KnowledgeId, a => a.MapFrom(ca => ca.KnowledgeId))
                .ForMember(am => am.Knowledge, a => a.Ignore())
                //.ForMember
                .ReverseMap();
            
            CreateMap<Knowledge, KnowledgesModel>()
                .ForMember(p => p.KnowledgeId, c => c.MapFrom(ca => ca.KnowledgeId))
                .ForMember(am => am.KnowledgeName, a => a.MapFrom(ca => ca.KnowledgeName))
                .ForMember(am => am.AllTestId, a => a.MapFrom(ca => ca.AllTestId))
                //.ForMember(am => am.AllTest, a => a.Ignore())
                .ReverseMap();

            CreateMap<ApplicationRole, RoleModel>()
                .ForMember(p => p.Name, c => c.MapFrom(ca => ca.Name))
                .ForMember(am => am.NormalizedName, c => c.MapFrom(ca => ca.NormalizedName))
                .ReverseMap();

            CreateMap<Role, RoleModel>()
                .ForMember(p => p.Name, c => c.MapFrom(ca => ca.Name))
                .ForMember(am => am.NormalizedName, a => a.Ignore())
                .ReverseMap();

            CreateMap<KnowledgeResult, KnowledgeResultModel>()
                .ForMember(am => am.Id, c => c.MapFrom(ca => ca.KnowledgeResultId))
                .ForMember(p => p.Date, c => c.MapFrom(ca => ca.Date))
                .ForMember(am => am.KnowledgeName, c => c.MapFrom(ca => ca.Knowledge.KnowledgeName))
                .ForMember(am => am.TotalResult, c => c.MapFrom(ca => ca.Result))
                .ForMember(am => am.Date, c => c.MapFrom(ca => ca.Date))
                .ReverseMap();
            CreateMap<Knowledge, KnowledgeResultModel>()
                .ForMember(a => a.KnowledgeName, b => b.MapFrom(c => c.KnowledgeName))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.KnowledgeId))
                .ReverseMap();
            CreateMap<QuestionResult, QuestionResultModel>()
                .ForMember(am => am.Id, c => c.MapFrom(ca => ca.QuestionResultId))
                .ForMember(p => p.Result, c => c.Ignore())
                .ForMember(am => am.QuestionString, c => c.MapFrom(ca => ca.Question.QuestionString))
                .ForMember(am => am.QuestionId, c => c.MapFrom(ca => ca.QuestionId))
                .ForMember(am => am.KnowledgeResultId, c => c.MapFrom(ca => ca.KnowledgeResultId))
                .ForMember(am => am.Answer, c => c.MapFrom(ca => ca.AnswerResult))
                .ReverseMap();

            CreateMap<Answer, AnswerResultModel>()
                .ForMember(a => a.AnswerId, b => b.MapFrom(c => c.AnswerId))
                .ForMember(a => a.AnswerString, b => b.MapFrom(c => c.AnswerString))
                .ForMember(a => a.CorrectAnswer, b => b.MapFrom(c => c.CorrectAnswer))
                .ReverseMap();

            CreateMap<AnswerResult, Answer>()
                .ForMember(a => a.AnswerId, b => b.MapFrom(c => c.AnswerId))
                .ForMember(a => a.AnswerString, b => b.MapFrom(c => c.Answer.AnswerString))
                .ForMember(a => a.CorrectAnswer, b => b.MapFrom(c => c.Answer.CorrectAnswer))
                .ForMember(a => a.QuestionId, b => b.MapFrom(c => c.Answer.QuestionId))
                .ReverseMap();

            CreateMap<AnswerResult, AnswerResultModel>()
                .IncludeMembers(s => s.Answer)
                .ForMember(am => am.Id, c => c.MapFrom(ca => ca.AnswerResultId))
                .ForMember(am => am.AnswerString, c => c.MapFrom(ca => ca.Answer.AnswerString))
                .ForMember(am => am.CorrectAnswer, c => c.MapFrom(ca => ca.Answer.CorrectAnswer))
                .ForMember(am => am.AnswerId, c => c.MapFrom(ca => ca.Answer.AnswerId))
                .ForMember(am => am.QuestionResultModelId, c => c.MapFrom(ca => ca.QuestionResultId))
                .ReverseMap();

            CreateMap<ApplicationUser, User>()
                .ForMember(p => p.Username, c => c.MapFrom(ca => ca.UserName))
                .ForMember(am => am.Id, c => c.MapFrom(ca => ca.Id))
                .ReverseMap();

            CreateMap<ApplicationUser, UserModel>()
               .ForMember(p => p.UserName, c => c.MapFrom(ca => ca.UserName))
               .ForMember(am => am.Email, c => c.MapFrom(ca => ca.Email));

        }
    }
}
