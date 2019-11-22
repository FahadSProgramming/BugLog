using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BugLog.Application;
using BugLog.Application.Interfaces;
using BugLog.Services.CaseServices;
using BugLog.Persistence;
using AutoMapper;
using MediatR;
using System.Reflection;
using BugLog.Application.Cases.Queries;
using FluentValidation.AspNetCore;
using BugLog.Application.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BugLog.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using BugLog.WebApi.Infrastructure;

namespace BugLog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(new Assembly[]{ typeof(MappingProfile).GetTypeInfo().Assembly});
            
            // Add MediatR
            services.AddMediatR(typeof(BugLog.Application.Cases.Queries.GetCaseDetailQueryHandler).GetTypeInfo().Assembly);
          
            // Add CaseSLAService
            services.AddTransient<ISLACalculationService, SLACalculatorService>();
            
            // Add fluent validation behavour implementation.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            
            // Add DbContext
            services.AddDbContext<IBugLogDbContext, BugLogDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("BugLogLiteDb")));

            // Register services
            services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();
            services.AddScoped<IPasswordGenerationService, PasswordGenerationService>();
            services.AddScoped<ISystemUserAccessorService, SystemUserAccessorService>();

            // Add Jwt token authentication
            // services.AddAuthentication(options => {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options. DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // })
            // .AddJwtBearer(options => {
            //     options.SaveToken = true;
            //     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("mysuperdupersecretkeycomesfromappsettingsconfig")),
            //         ValidateIssuer = false,
            //         ValidateAudience = false,
            //         ValidateLifetime = true
            //     };
            // });


            services.AddControllers(opt => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IBugLogDbContext>());

             var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("n:LY;6p(c<EGC6TX+ER>$bj,@SWQ4Q!@{6RH;B#{s9C7mUqd"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            // Inject custom exception handling middlewae
            app.UseExceptionHandling();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
