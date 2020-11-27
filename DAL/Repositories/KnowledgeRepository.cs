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
    public class KnowledgeRepository : IKnowledgeRepository
    {
        private readonly ApplicationDbContext db;
        public KnowledgeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(Knowledge entity)
        {
            await Task.Run(() => db.Knowledges.Add(entity));
        }

        public void Delete(Knowledge entity)
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
                db.Knowledges.Remove(db.Knowledges.Find(id));
            });
        }

        public IQueryable<Knowledge> FindAll()
        {
            return db.Knowledges;
        }

        public Task<Knowledge> GetByIdAsync(int id)
        {
            return Task.Run(() => {

                Knowledge knowledge = db.Knowledges.Find(id);
                return knowledge;
            });
        }

        public void Update(Knowledge entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
