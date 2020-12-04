using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using System.Reflection;
using Intuitive.Domain.Interfaces;
using Intuitive.Domain.Repository;
using Intuitive.Domain.PipelineBehaviors;
using Microsoft.EntityFrameworkCore;
using Intuitive.Domain.Commands;

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

            services.AddMediatR(typeof(GetAllUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetUserByIdCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteUserCommand).GetTypeInfo().Assembly);
            services.AddControllers();
            services.AddScoped(typeof(IIntuitiveRepository), typeof(IntuitiveRepository));
            services.AddDbContext<IntuitiveContext>(op => {
                op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
