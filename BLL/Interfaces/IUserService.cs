using BLL.Infrastructure;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserModel userDto);
        Task CreateAdmin(UserModel userDto);
        Task<ApplicationUser> GetUserByEmail(string email);
        //Task<bool> Login(LoginModel model);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        ApplicationUser GetById(int id);
        string generateJwtToken(ApplicationUser user);
        //Task<ClaimsIdentity> Authenticate(UserModel userDto);
        //Task SetInitialData(UserModel adminDto, List<string> roles); T
    }
}
