using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public readonly ApplicationDbContext db;
        private IAllTestsRepository allTestsRepository;
        private IKnowledgeRepository knowledgeRepository;
        private IAnswersRepository answersReposistory;
        private IQuestionRepository questionRepository;
        private IKnowledgeResultsRepository knowledgeResultRepository;
        private IAnswerResultsRepository answersResultsReposistory;
        private IQuestionResultsRepository questionResultRepository;
        private ITestResultsRepository testResultsRepository;
        private IUserRepository userRepositry;
        private IRoleRepository roleRepositry;
        //private ApplicationUserManager userManager;
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ITestResultsRepository TestResultsRepository
        {
            get
            {
                if (testResultsRepository == null)
                    testResultsRepository = new TestResultsRepository(db);

                return testResultsRepository;
            }
        }

        public IAllTestsRepository AllTestsRepository
        {
            get
            {
                if (allTestsRepository == null)
                    allTestsRepository = new AllTestsRepository(db);

                return allTestsRepository;
            }
        }
        public IKnowledgeRepository KnowledgeRepository
        {
            get
            {
                if (allTestsRepository == null)
                    knowledgeRepository = new KnowledgeRepository(db);

                return knowledgeRepository;
            }
        }

        public IAnswersRepository AnswersRepository
        {
            get
            {
                if (answersReposistory == null)
                    answersReposistory = new AnswersRepository(db);

                return answersReposistory;
            }
        }

        public IQuestionRepository QuestionRepository
        {
            get
            {
                if (questionRepository == null)
                    questionRepository = new QuestionRepository(db);

                return questionRepository;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                if (roleRepositry == null)
                    roleRepositry = new RoleRepository(db);

                return roleRepositry;
            }
        }


        public IKnowledgeResultsRepository KnowledgeResultRepository
        {
            get
            {
                if (knowledgeResultRepository == null)
                    knowledgeResultRepository = new KnowledgeResultsRepository(db);

                return knowledgeResultRepository;
            }
        }
        public IAnswerResultsRepository AnswersResultsReposistory
        {
            get
            {
                if (answersResultsReposistory == null)
                    answersResultsReposistory = new AnswerResultsRepository(db);

                return answersResultsReposistory;
            }
        }
        public IQuestionResultsRepository QuestionResultRepository
        {
            get
            {
                if (questionResultRepository == null)
                    questionResultRepository = new QuestionResultsRepository(db);

                return questionResultRepository;
            }
        }

        //new ApplicationUserManager(new UserStore<ApplicationUser>(db));

        public void Save()
        {
            db.SaveChanges();
        }

        public Task<int> SaveAsync()
        {

            return Task.Run(() => db.SaveChangesAsync());

        }
    }
}
