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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Store.Context;
using WebAPI.Store.IRepositories;
using WebAPI.Store.IServices;
using WebAPI.Store.Repositories;
using WebAPI.Store.Services;

namespace WebAPICore5
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
            var connentionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPICore5", Version = "v1" });
            });

            services.AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IProductService, ProductService>();

            services.AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IUserService, UserService>();

            services.AddTransient<StoreContext>(x => new StoreContext(connentionString, migrationAssemblyName));


            services.AddDbContext<StoreContext>(x => x.UseSqlServer(connentionString, m => m.MigrationsAssembly(migrationAssemblyName)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPICore5 v1"));
            }

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
