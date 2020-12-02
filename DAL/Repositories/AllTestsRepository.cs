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
    public class AllTestsRepository : IAllTestsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AllTestsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /*public Task AddAsync(AllTest entity)
        {
            throw new NotImplementedException();
        }*/


        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AllTest> FindAll()
        {
            return dbContext.AllTests;
        }

        public async Task AddAsync(AllTest entity)
        {
            await Task.Run(() => dbContext.AllTests.Add(entity));
        }

        public void Delete(AllTest entity)
        {
            if (entity != null)
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }

        public Task<AllTest> GetByIdAsync(int id)
        {
            return Task.Run(() => {

                AllTest answer = dbContext.AllTests.Find(id);
                return answer;
            });
        }

        public void Update(AllTest entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        
    }
}
