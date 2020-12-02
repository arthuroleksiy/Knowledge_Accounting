using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Context;
using Microsoft.AspNetCore.Identity;
using DAL.Repositories;
using BLL.Services;
using Microsoft.EntityFrameworkCore;
using BLL;
using System.Reflection;
using BLL.Infrastructure;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("WebApplication1")));


            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<IAllTestsRepository, AllTestsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IKnowledgeRepository, KnowledgeRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IKnowledgeResultsRepository, KnowledgeResultsRepository>();
            services.AddScoped<IAnswerResultsRepository, AnswerResultsRepository>();
            services.AddScoped<IQuestionResultsRepository, QuestionResultsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IKnowledgeService, KnowledgeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IQuestionToQuestionModel, QuestionToQuestionModel>();
            services.AddScoped<IKnowledgeToKnowledgeModel, KnowledgeToKnowledgeModel>();
            services.AddScoped<ITestResultsRepository, TestResultsRepository>();
            services.AddScoped<ITestResultService, TestResultService>();
            services.AddScoped<IAnswerResultService, AnswerResultService>();
            services.AddScoped<IQuestionResultService, QuestionResultService>();
            services.AddScoped<IKnowledgeResultToKnowledgeResultModel, KnowledgeResultToKnowledgeResultModel>();
            services.AddAutoMapper(typeof(AutoMapping).GetTypeInfo().Assembly);

            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();

             app.UseSwagger(c =>
             {
                 c.SerializeAsV2 = true;
             });
             
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
              



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite > SameSiteMode.Unspecified)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                    options.SameSite = SameSiteMode.Unspecified;
                
            }
        }

    }
}
