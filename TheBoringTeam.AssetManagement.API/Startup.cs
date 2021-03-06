﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
            services.AddAuthentication(Configuration);
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

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.SendFileAsync("wwwroot/index.html");
                }
            });
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
            services.AddTransient<IBaseMongoRepository<Right>>(f =>
                new BaseMongoRepository<Right>(configuration["mongoConnectionString"],
                    configuration["mongoDatabaseName"],
                    true));
            services.AddTransient<IBaseMongoRepository<Role>>(f =>
                new BaseMongoRepository<Role>(configuration["mongoConnectionString"],
                    configuration["mongoDatabaseName"],
                    true));
            services.AddTransient<IBaseMongoRepository<AssetError>>(f =>
                new BaseMongoRepository<AssetError>(configuration["mongoConnectionString"],
                    configuration["mongoDatabaseName"],
                    true));
            services.AddTransient<IBaseService<User>, BaseService<User>>();
            services.AddTransient<IBaseService<Asset>, BaseService<Asset>>();
            services.AddTransient<IBaseService<Role>, BaseService<Role>>();
            services.AddTransient<IBaseService<Right>, BaseService<Right>>();
            services.AddTransient<IBaseService<AssetError>, BaseService<AssetError>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRightService, RightService>();
            services.AddTransient<IAssetErrorService, AssetErrorService>();

        }
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["appSecret"]);
            services.AddAuthentication(f =>
            {
                f.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                f.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(f =>
            {
                f.RequireHttpsMetadata = false;
                f.SaveToken = true;
                f.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AssetsRead", p => p
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim("AssetsRead"));

                opts.AddPolicy("AssetsEdit", p => p
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim("AssetsEdit"));

                opts.AddPolicy("UsersRead", p => p
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim("UsersRead"));

                opts.AddPolicy("UsersEdit", p => p
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim("UsersEdit"));
            });

        }
    }
}
