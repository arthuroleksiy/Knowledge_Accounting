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
    public class TestResultsRepository : ITestResultsRepository
    {
        private readonly ApplicationDbContext db;
        public TestResultsRepository(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }
        public async Task AddAsync(KnowledgeResult entity)
        {
            await Task.Run(() => db.KnowledgeResults.Add(entity));
        }

        public void Delete(KnowledgeResult entity)
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
                db.KnowledgeResults.Remove(db.KnowledgeResults.Find(id));
            });
        }

        public IQueryable<KnowledgeResult> FindAll()
        {
            return db.KnowledgeResults;
        }
       
        public IQueryable<KnowledgeResult> FindByDateAndPoint(int minPoint, int maxPoint, DateTime minDate, DateTime maxDate)
        {
            return db.KnowledgeResults.Where(i => i.Date > minDate && i.Date < maxDate && i.Result > minPoint && i.Result < maxPoint).Select(j => j);
        }

        public async Task<KnowledgeResult> GetByIdAsync(int id)
        {
            return await Task.Run(() => db.KnowledgeResults.Where(i => i.KnowledgeResultId == id).Select(j => j).FirstOrDefault());

        }

        public void Update(KnowledgeResult entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
