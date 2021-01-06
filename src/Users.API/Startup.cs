using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Users.Infrastructure.MongoDB.Connection;
using Users.Infrastructure.MongoDB.Repositories;
using Users.Models.Settings;
using Users.Repositories;

namespace Users.API
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
            AddConfigSections(services);
            AddRepositories(services);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options =>
            {
                // return status code 406 for unsupported Accept header types
                options.ReturnHttpNotAcceptable = true;

                // add default response types for all endpoints
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status415UnsupportedMediaType));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            })
                // for JsonPatch
                .AddNewtonsoftJson()
                // add support for application/xml Accept header type
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users.API", Version = "v1" });

                var apiXmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, "Users.API.xml");
                var coreXmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, "Users.Core.xml");
                c.IncludeXmlComments(apiXmlCommentsFullPath);
                c.IncludeXmlComments(coreXmlCommentsFullPath);
            });

            services.AddHealthChecks()
                .AddMongoDb(
                    mongodbConnectionString: Configuration[$"{UserRepositorySettings.SectionName}:{nameof(UserRepositorySettings.ConnectionString)}"],
                    name: "mongo",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new string[] { "db", "nosql", "mongodb" });
            
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users.API v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapControllers();
            });
        }

        private void AddConfigSections(IServiceCollection services)
        {
            services.Configure<UserRepositorySettings>(Configuration.GetSection(UserRepositorySettings.SectionName));
            services.AddSingleton<IUserRepositorySettings>(sp => sp.GetRequiredService<IOptions<UserRepositorySettings>>().Value);
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddSingleton<IMongoDBUserRepositoryConnection, MongoDBUserRepositoryConnection>();
            services.AddSingleton<IUserRepository, MongoDBUserRepository>();
        }
    }
}
