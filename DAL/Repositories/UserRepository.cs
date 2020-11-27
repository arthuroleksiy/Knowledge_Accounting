using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddRole(/*User model*/)
        { 
            /*ApplicationUser applicationUser = new ApplicationUser()
            applicationUser.UserName = "wx@hotmail.com";
            applicationUser.Email = "wx@hotmail.com";
            applicationUser.NormalizedUserName = "wx@hotmail.com";

            dbContext.Users.Add(applicationUser);


            var hasedPassword = _passwordHasher.HashPassword(applicationUser, "YourPassword");
            applicationUser.SecurityStamp = Guid.NewGuid().ToString();
            applicationUser.PasswordHash = hasedPassword;

            _context.SaveChanges();
            */

            //dbContext..user(new IdentityUser<int>());
            //await dbContext.Users.Add(new ApplicationUser {  Email = model.Email, UserName = model.UserName,  });
            await dbContext.SaveChangesAsync();
        }
        
    }
}
