using DAL.Context;
using DAL.Entities.Results;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class QuestionResultsRepository: IQuestionResultsRepository
    {
        private readonly ApplicationDbContext db;
        public QuestionResultsRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(QuestionResult entity)
        {
            await Task.Run(() => db.QuestionResults.Add(entity));
        }

        public void Delete(QuestionResult entity)
        {
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Task DeleteByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                db.QuestionResults.Remove(db.QuestionResults.Find(id));
            });
        }

        public IQueryable<QuestionResult> FindAll()
        {
            return db.QuestionResults;
        }

        public Task<QuestionResult> GetByIdAsync(int id)
        {
            return Task.Run(() => {

                QuestionResult book = db.QuestionResults.Find(id);
                return book;
            });
        }

        public void Update(QuestionResult entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
