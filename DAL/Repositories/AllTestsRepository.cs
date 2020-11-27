using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
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
        public Task AddAsync(AllTest entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AllTest entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AllTest> FindAll()
        {
            return dbContext.AllTests;
        }

        public Task<AllTest> GetByIdAsync(int id)
        {

            throw new NotImplementedException();
        }

        public void Update(AllTest entity)
        {
            throw new NotImplementedException();
        }
    }
}
