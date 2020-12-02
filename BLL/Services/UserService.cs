using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        UserManager<ApplicationUser> UserManager { get; set; }
        RoleManager<ApplicationRole> RoleManager { get; set; }

        private readonly AppSettings appSettings;
        IMapper Mapper { get; set; }

        public UserService(IUnitOfWork uow, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> RoleManager, IOptions<AppSettings> appSettings, IMapper Mapper)
        {
            Database = uow;
            UserManager = userManager;
            this.appSettings = appSettings.Value;
            this.RoleManager = RoleManager;
            this.Mapper = Mapper;
        }
        public async Task CreateUser(UserModel userDto)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Name };
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.NormalizedEmail = userDto.Email;
                user.NormalizedUserName = userDto.UserName;
                var result = await UserManager.CreateAsync(user, userDto.Password);
            }

            var i = await RoleManager.FindByNameAsync("User");

            await UserManager.AddToRoleAsync(user, i.Name);
        }

        public async Task CreateAdmin(UserModel userDto)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Name };
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.NormalizedEmail = userDto.Email;
                user.NormalizedUserName = userDto.UserName;
                var result = await UserManager.CreateAsync(user, userDto.Password);
            }

            var i = await RoleManager.FindByNameAsync("Admin");
            await UserManager.AddToRoleAsync(user, i.Name);
        }


        public async Task<User> Authenticate(AuthenticateRequest model)
        {
            var user = UserManager.Users.Where(x => x.UserName == model.Username).FirstOrDefault();

            if (await UserManager.CheckPasswordAsync(user, model.Password))
            {
                if (user == null) 
                    return null;
            }
            var resultUser = Mapper.Map<User>(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, UserManager.GetRolesAsync(user).Result.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            resultUser.Token = tokenHandler.WriteToken(token);

            return resultUser.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            var result =  UserManager.Users;
            return Mapper.Map<IEnumerable<ApplicationUser>, List<User>>(result);

        }

        public ApplicationUser GetById(int id)
        {
            return UserManager.Users.FirstOrDefault(x => x.Id == id);
        }

        

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return UserManager.Users.FirstOrDefault(x => x.Email == email);
        }




    }
}

