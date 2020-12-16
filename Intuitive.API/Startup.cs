using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using System.Reflection;
using Intuitive.Domain.Interfaces;
using Intuitive.Domain.Identity;
using Intuitive.Repository;
using Microsoft.EntityFrameworkCore;
using Intuitive.Domain.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Intuitive.API.Helpers;
using System;
using System.IO;
using Microsoft.OpenApi.Models;
using Intuitive.Domain.Commands.AuthenticationCommands;

namespace Intuitive.API
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
            
            services.AddMediatR(typeof(RegisterCommands).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(LoginCommands).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetUserByIdCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteUserCommand).GetTypeInfo().Assembly);
            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped(typeof(IIntuitiveRepository), typeof(IntuitiveRepository));
            services.AddDbContext<IntuitiveContext>(op => {
                op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            IdentityBuilder builder = services.AddIdentityCore<UserApplication>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<IntuitiveContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<UserApplication>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
            
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "IntuitiveAPI", 
                    Version = "v1",
                    Description = "Projeto de demonstração ASP.Net Core"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    
                c.IncludeXmlComments(xmlPath);

                

                c.AddSecurityDefinition(

                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Copie 'Bearer ' + token'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"

                    });
                        
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

               //Ativa o Swagger
            app.UseSwagger();

            // Ativa o Swagger UI
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "IntuitiveAPI V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
