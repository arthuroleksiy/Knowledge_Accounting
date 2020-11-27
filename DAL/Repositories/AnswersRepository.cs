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
    public class AnswersRepository : IAnswersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AnswersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Answer entity)
        {
            await Task.Run(() => dbContext.Answers.Add(entity));
        }

        public void Delete(Answer entity)
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

        public IQueryable<Answer> FindAll()
        {
            return dbContext.Answers;
        }

        public Task<Answer> GetByIdAsync(int id)
        {
            return Task.Run(() => {

                Answer answer = dbContext.Answers.Find(id);
                return answer;
            });
        }

        public void Update(Answer entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
