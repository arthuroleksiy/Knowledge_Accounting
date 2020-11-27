using DAL.Context;
using DAL.Entities;
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
    public class AnswerResultsRepository: IAnswerResultsRepository
    {

        private readonly ApplicationDbContext dbContext;
        public AnswerResultsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(AnswerResult entity)
        {
            await Task.Run(() => dbContext.AnswerResults.Add(entity));
        }

        public void Delete(AnswerResult entity)
        {
            if (entity != null)
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }

        public Task DeleteByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                dbContext.Answers.Remove(dbContext.Answers.Find(id));
            });
        }

        public IQueryable<AnswerResult> FindAll()
        {
            return dbContext.AnswerResults;
        }

        public Task<AnswerResult> GetByIdAsync(int id)
        {
            return Task.Run(() => {

                AnswerResult answer = dbContext.AnswerResults.Find(id);
                return answer;
            });
        }

        public void Update(AnswerResult entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
