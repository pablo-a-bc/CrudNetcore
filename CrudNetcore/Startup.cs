using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudNetcore.Datos;
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

namespace CrudNetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string AllowSpecificOrigins = "AllowOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options => {
                options.AddPolicy(AllowSpecificOrigins,
                    builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://localhost:80/", "http://localhost:443")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Agendamiento", Version = "v1" });
            });
            services.ConfigureSwaggerGen(options => {
                options.CustomSchemaIds(x => x.FullName);
            });
            services.AddDbContext<crudContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("Database")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Api Crud");
                //c.RoutePrefix = "apiagenda";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(AllowSpecificOrigins);
            });
        }
    }
}
