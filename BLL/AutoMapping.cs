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
                // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
                .ForMember(p => p.AnswerId, c => c.MapFrom(ca => ca.AnswerId))
                .ForMember(am => am.AnswerString, a => a.MapFrom(ca => ca.AnswerString))
                .ForMember(am => am.CorrectAnswer, a => a.MapFrom(ca => ca.CorrectAnswer))
                .ForMember(am => am.QuestionId, a => a.MapFrom(ca=> ca.QuestionId))
                .ForMember(am => am.Question, a => a.Ignore())
                .ReverseMap();


            
            CreateMap<Question, QuestionsModel>()
                // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
                .ForMember(p => p.QuestionId, c => c.MapFrom(ca => ca.QuestionId))
                .ForMember(am => am.QuestionString, a => a.MapFrom(ca => ca.QuestionString))
                .ForMember(am => am.KnowledgeId, a => a.MapFrom(ca => ca.KnowledgeId))
                .ForMember(am => am.Knowledge, a => a.Ignore())
                //.ForMember
                .ReverseMap();
            
            CreateMap<Knowledge, KnowledgesModel>()
                // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
                .ForMember(p => p.KnowledgeId, c => c.MapFrom(ca => ca.KnowledgeId))
                .ForMember(am => am.KnowledgeName, a => a.MapFrom(ca => ca.KnowledgeName))
                .ForMember(am => am.AllTestId, a => a.MapFrom(ca => ca.AllTestId))
                .ForMember(am => am.AllTest, a => a.Ignore())
                //.ForMember(am => am.KnowledgeUser, a => a.Ignore())
                .ReverseMap();

            CreateMap<ApplicationRole, RoleModel>()
                // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
                .ForMember(p => p.Name, c => c.MapFrom(ca => ca.Name))
                .ForMember(am => am.NormalizedName, a => a.Ignore())
                //.ForMember(am => am.KnowledgeUser, a => a.Ignore())
                .ReverseMap();

            CreateMap<Role, RoleModel>()
                // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
                .ForMember(p => p.Name, c => c.MapFrom(ca => ca.Name))
                .ForMember(am => am.NormalizedName, a => a.Ignore())
                //.ForMember(am => am.KnowledgeUser, a => a.Ignore())
                .ReverseMap();

            CreateMap<KnowledgeResult, KnowledgeResultModel>()
                // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
                .ForMember(p => p.Date, c => c.MapFrom(ca => ca.Date))
                .ForMember(am => am.KnowledgeName, c => c.MapFrom(ca => ca.KnowledgeName))
                .ForMember(am => am.Result, c => c.MapFrom(ca => ca.Result))
                //.ForMember(am => am.KnowledgeUser, a => a.Ignore())
                .ReverseMap();

            CreateMap<ApplicationUser, User>()
                // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
                .ForMember(p => p.Username, c => c.MapFrom(ca => ca.UserName))
                .ForMember(am => am.Id, c => c.MapFrom(ca => ca.Id)).ReverseMap();
                //.ForMember(am => am., c => c.MapFrom(ca => ca));

            CreateMap<ApplicationUser, UserModel>()
               // .ForMember(p => p., c => c.MapFrom(card => card.Cards.Select(x => x.CardId)))
               .ForMember(p => p.UserName, c => c.MapFrom(ca => ca.UserName))
               .ForMember(am => am.Email, c => c.MapFrom(ca => ca.Email));
            //.ForMember(am => am.KnowledgeUser, a => a.Ignore())

        }
    }
}
