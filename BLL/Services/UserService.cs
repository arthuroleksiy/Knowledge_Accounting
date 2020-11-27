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
                //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Database));


                //if (result.Succeeded == false)
                //   return new OperationDetails(false, , "");
                //var i = await RoleManager.FindByNameAsync("User");
                // добавляем роль
                //await UserManager.AddToRoleAsync(user, i.Name);
                //создаем профиль клиента
                //ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                //Database.ClientManager.Create(clientProfile);
            }
            //await Database.SaveAsync();

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
                //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Database));


                //if (result.Succeeded == false)
                //   return new OperationDetails(false, , "");
                //var i = await RoleManager.FindByNameAsync("User");
                // добавляем роль
                //await UserManager.AddToRoleAsync(user, i.Name);
                //создаем профиль клиента
                //ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                //Database.ClientManager.Create(clientProfile);
            }
            //await Database.SaveAsync();

            var i = await RoleManager.FindByNameAsync("Admin");
            // добавляем роль
            await UserManager.AddToRoleAsync(user, i.Name);
        }


        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = UserManager.Users.Where(x => x.UserName == model.Username).FirstOrDefault();

            if (await UserManager.CheckPasswordAsync(user, model.Password))
            {
                if (user == null) 
                    return null;

                
            }
            var token = generateJwtToken(user);
            return new AuthenticateResponse(Mapper.Map<User>(user), token);
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

        // helper methods

        public string generateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return UserManager.Users.FirstOrDefault(x => x.Email == email);
        }


       
        //public async Task<bool> Login([FromBody]LoginModel model)
        //{


        /*if (!ModelState.IsValid || credentials == null)
        {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }*/
        /*
        var identityUser = await UserManager.FindByNameAsync(credentials.Username);

        if (identityUser == null) 
        { 
            return new BadRequestObjectResult(new { Message = "Login failed" }); 
        }
        var result = UserManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password); 
        if (result == PasswordVerificationResult.Failed) 
        { 
            return new BadRequestObjectResult(new { Message = "Login failed" }); 
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, identityUser.Email), new Claim(ClaimTypes.Name, identityUser.UserName)
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Ok(new { Message = "You are logged in" });*/
        /*ClaimsIdentity claim = null;
        ApplicationUser user = await UserManager.FindByEmailAsync(model.Email).Wait().HasPasswordAsync(model.Password);

        if (user != null)

            claim = await UserManager.CreateIdentityAsync(user,
                                        DefaultAuthenticationTypes.ApplicationCookie);
        return claim;*/
        // }
        /*public async Task<ClaimsIdentity> Authenticate([FromBody] LoginModel userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await SignInManager.PasswordSignInAsync.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }*/
        /*
        public Task AddAsync(QuestionsModel model)
        {
            //Question question = mapper.Map<Question>(model);

            return Task.Run(() => {
                UnitOfWork.QuestionRepository.AddAsync(question);
                UnitOfWork.SaveAsync();
            });
        }

        

        public IEnumerable<QuestionsModel> GetAll()
        {
            var result = UnitOfWork.QuestionRepository.FindAll();
            

        }*/

        //RoleManager<ApplicationRole> _roleManager;
        /*
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(RoleManager<ApplicationRole> manager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = manager;
           
            _userManager = userManager;
        
            _signInManager = signInManager;
        }*/

        /*
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

        }*/


        /*public async Task<IActionResult> GetRoles()
        {
            _roleManager.CreateAsync(new IdentityRole { Name = "User", NormalizedName = "USER" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
        }*/

        /*
        public async Task<OperationDetails> Create(UserModel userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        */
        // начальная инициализация бд


    }
}

