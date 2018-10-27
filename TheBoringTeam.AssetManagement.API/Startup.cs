﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Repositories.Entities;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Entities;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public static class Extensions
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBaseMongoRepository<User>>(f =>
                new BaseMongoRepository<User>(configuration["mongoConnectionString"],
                    configuration["mongoDatabaseName"],
                    true));
            services.AddTransient<IBaseMongoRepository<Asset>>(f =>
                new BaseMongoRepository<Asset>(configuration["mongoConnectionString"],
                    configuration["mongoDatabaseName"],
                    true));
            services.AddTransient<IBaseService<User>, BaseService<User>>();
            services.AddTransient<IBaseService<Asset>, BaseService<Asset>>();
            services.AddTransient<IBaseService<Role>, BaseService<Role>>();
            services.AddTransient<IBaseService<Right>, BaseService<Right>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRightService, RightService>();
        }
    }
}
