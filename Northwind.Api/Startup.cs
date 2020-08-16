using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Northwind.Api.Extensions;
using Northwind.Api.Models;
using Northwind.Data.Entities;
using Swashbuckle.AspNetCore.Swagger;

namespace Northwind.Api
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
            services.AddMvc();
            // Northwind Database connection
            services.AddDataAccessServices(Configuration);
            services.RegisterServices();

            services.AddControllers();

            // Register swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Supplement API",
                    Version = "v1",
                    Description = "Supplement API tutorial using MongoDB",
                });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Supplement V1");
            });

            // Adding this makes graphiql UI available at /graphql 
            // https://github.com/JosephWoodward/graphiql-dotnet
            app.UseGraphiQl();

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

// TODO: testing
// https://thecodebuzz.com/mongodb-driver-mocking-unit-testing-iasynccursor-async-method/
// https://thecodebuzz.com/mongodb-driver-mocking-unit-testing-iasynccursor-async-method-part2/
// UoW pattern: https://github.com/brunohbrito/MongoDB-RepositoryUoWPatterns/blob/master/MongoDB.GenericRepository/Startup.cs
// https://bryanavery.co.uk/asp-net-core-mongodb-repository-pattern-unit-of-work/