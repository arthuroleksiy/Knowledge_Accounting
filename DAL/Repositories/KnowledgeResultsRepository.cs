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
    public class KnowledgeResultsRepository: IKnowledgeResultsRepository
    {
        private readonly ApplicationDbContext db;
        public KnowledgeResultsRepository(ApplicationDbContext db)
        {
            this.db = db;
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

        public Task<KnowledgeResult> GetByIdAsync(int id)
        {
            return Task.Run(() => {

                KnowledgeResult knowledge = db.KnowledgeResults.Find(id);
                return knowledge;
            });
        }

        public void Update(KnowledgeResult entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
