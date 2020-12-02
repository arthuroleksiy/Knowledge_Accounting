using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
         IAllTestsRepository AllTestsRepository { get; }
         IKnowledgeRepository KnowledgeRepository { get; }
         IAnswersRepository AnswersRepository { get; }
         IQuestionRepository QuestionRepository { get; }
         IRoleRepository RoleRepository { get; }
         IKnowledgeResultsRepository KnowledgeResultRepository { get; }
         IAnswerResultsRepository AnswersResultsReposistory { get; }
         IQuestionResultsRepository QuestionResultRepository { get; }
         ITestResultsRepository TestResultsRepository { get; }
        //UserManager<ApplicationUser> UserManager { get; }
        //SignInManager<ApplicationUser> SignInManager { get; }
        Task<int> SaveAsync();
    }
}
