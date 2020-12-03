using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext db;
        public QuestionRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Question entity)
        {
            await Task.Run(() => db.Questions.Add(entity));
        }

        public void Delete(Question entity)
        {
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Task DeleteAnswerByQuestionIdAsync(int id)
        {
            return Task.Run(() =>
            {
                var questions = db.Questions.Find(id);

                if (questions.Answers.Count > 0) {

                    do
                    {
                        var answer = questions.Answers.First();
                        questions.Answers.Remove(answer);
                    }
                    while (questions.Answers.Count > 0);
                }
            });
        }

        public Task DeleteByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                db.Questions.Remove(db.Questions.Find(id));
            });
        }

        public IQueryable<Question> FindAll()
        {
            return db.Questions;
        }

        public Task<Question> GetByIdAsync(int id)
        {
            return Task.Run(() => {

                Question book = db.Questions.Find(id);
                return book;
            });
        }

        public void Update(Question entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
