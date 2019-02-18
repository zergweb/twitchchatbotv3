using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwitchBotV3.DbInit;
using TwitchBotV3.Model.Repositories;
using System.Text;
using TwitchBotV3.Middlewares;
using TwitchBotV3.Services.WebSocketService;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading;
using TwitchBotV3.Logger;

namespace TwitchBotV3
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidIssuer = Configuration["JwtToken:Issuer"],
                           ValidateAudience = true,
                           ValidAudience = Configuration["JwtToken:Audience"],
                           ValidateLifetime = false,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:Key"])),
                           ValidateIssuerSigningKey = true
                       };
                   });
            services.AddDbContext<AppDbContext>();
            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
            services.AddMvc();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            loggerFactory.WebSocketLogger(serviceProvider.GetService<ChatMessageHandler>());
            DbInitializer.InitDb(app);
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseWebSockets();
            app.MapWebSocket("/ws", serviceProvider.GetService<ChatMessageHandler>());
            app.UseMvc();
            app.UseStaticFiles();
            
        }
      
    }
}
