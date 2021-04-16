using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Store.Context;
using WebAPI.Store.IRepositories;
using WebAPI.Store.IServices;
using WebAPI.Store.Repositories;
using WebAPI.Store.Services;
using WebAPICore5.Authorization;
using WebAPICore5.Identity;
using WebAPICore5.Models;
using WebAPICore5.Validations;

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


            var jwtOption = new JwtOption();
            Configuration.GetSection("jwt").Bind(jwtOption);

            services.AddSingleton(jwtOption);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOption.JwtIssuer,
                    ValidAudience = jwtOption.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.JwtKey))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "Bangladesh"));
                options.AddPolicy("AtLeast18", builder => builder.AddRequirements(new MinimumageRequirement(18)));

            });

            

            services.AddControllers().AddFluentValidation();

            // added this line for custome fluentValidation to work
            services.AddTransient<IValidator<RegisterModel>, RegisterUserValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPICore5", Version = "v1" });
            });

            services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IJwtProvider, JwtProvvider>();

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

            app.UseAuthentication();
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
