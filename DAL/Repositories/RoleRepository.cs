using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly ApplicationDbContext dbContext;
        public RoleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Role role)
        {
            dbContext.Add(new ApplicationRole(role.Name));
            await dbContext.SaveChangesAsync();
        }
        public IEnumerable<ApplicationRole> GetRoles()
        {
            return dbContext.Roles;
        }

    }
}
