using AutoMapper;
using BlazorCrud.Server.Services;
using BlazorCrud.Shared.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlazorCrud.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PatientContext>(opt => opt.UseInMemoryDatabase("PatientDb"));
            services.AddDbContext<OrganizationContext>(opt => opt.UseInMemoryDatabase("OrganizationDb"));
            services.AddDbContext<ClaimContext>(opt => opt.UseInMemoryDatabase("ClaimDb"));
            services.AddDbContext<UploadContext>(opt => opt.UseInMemoryDatabase("UploadDb"));
            services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("UserDb"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Blazor CRUD API",
                    Version = "v1",
                    Description = "CRUD API Services that act as the backend to the Blazor CRUD website."
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            // Add Quartz-Scheduled Services
            services.AddSingleton<HeatbeatJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HeatbeatJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<ProcessFileJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(ProcessFileJob),
                cronExpression: "0 0/1 * * * ?"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }           

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();

            // Serves up Swagger documentation at http://localhost:<port>/swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazore CRUD API v1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
    }
}
