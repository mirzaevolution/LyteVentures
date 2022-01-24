using GlobalExceptionHandler.WebApi;
using LyteVentures.Todo.Api.Filters;
using LyteVentures.Todo.Api.Models;
using LyteVentures.Todo.DataStorageLayers;
using LyteVentures.Todo.Repositories.Implementations;
using LyteVentures.Todo.Repositories.Interfaces;
using LyteVentures.Todo.Services.Implementations;
using LyteVentures.Todo.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Api
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ITodoScheduleRepository, TodoScheduleRepository>();
            services.AddScoped<ITodoScheduleService, TodoScheduleService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                      {
                          options.Authority = Configuration["IDP:BaseAddress"];
                          options.Audience = Configuration["IIDP:Audience"];
                          options.TokenValidationParameters.NameClaimType = "name";
                      });
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalModelValidatorFilter));
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LyteVentures.Todo.Api (Todo Schedule)", Version = "v1" });
                c.AddSecurityDefinition(Configuration["IDP:SwaggerKey"], new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{Configuration["IDP:BaseAddress"]}/connect/authorize"),
                            TokenUrl = new Uri($"{Configuration["IDP:BaseAddress"]}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { Configuration["IDP:Scope"],"Todo Schedule Services Api" }
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseGlobalExceptionHandler(options =>
            {
                options.ContentType = "application/json";
                options.ResponseBody((exception, httpContext) => JsonConvert.SerializeObject(new BaseResponse
                {
                    IsSuccess = false,
                    Message = exception.Message
                }));
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LyteVentures.Todo.Api (Todo Schedule) v1");
                c.OAuthClientId(Configuration["IDP:ClientId"]);
                c.OAuthClientSecret(Configuration["IDP:ClientSecret"]);
                c.OAuthAppName("Todo Schedule Services");
                c.OAuthUsePkce();

            });
            app.UseHttpsRedirection();

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
