using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RetailProductMicroservice.Api.Configuration;
using RetailProductMicroservice.Api.Services;
using RetailProductMicroservice.API.Controllers;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Application.Services;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Infrastructure.Data;
using RetailProductMicroservice.Infrastructure.Repositories;

namespace RetailProductMicroservice.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            var appSettings = new AppSettings(_configuration);

            bool useInMemoryDatabase = appSettings.UseInMemoryDatabase;
            if (useInMemoryDatabase)
            {
                services.AddDbContext<RetailProductContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDbForTesting"));
            }
            else
            {
                services.AddDbContext<RetailProductContext>(options =>
                    options.UseMySql(appSettings.DefaultConnection, 
                        new MySqlServerVersion(new Version(8, 0, 40))));
            }

            services.AddScoped<IAlmacenService, AlmacenService>();
            services.AddScoped<IAnaquelService, AnaquelService>();
            services.AddScoped<IExistenciaService, ExistenciaService>();
            services.AddScoped<IMovimientoService, MovimientoService>();
            services.AddScoped<ISerializableService, SerializableService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IAlmacenRepository, AlmacenRepository>();
            services.AddScoped<IAnaquelRepository, AnaquelRepository>();
            services.AddScoped<IExistenciaRepository, ExistenciaRepository>();
            services.AddScoped<IMovimientoRepository, MovimientoRepository>();
            services.AddScoped<ISerializableRepository, SerializableRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddSingleton<AppSettings>();

            services.AddScoped<AlmacenController>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RetailProductMicroservice API", Version = "v1" });
            });

            services.AddSingleton<DatabaseInitializer>();
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseInitializer databaseInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAllOrigins");

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthz");
                endpoints.MapHealthChecks("/ready", new HealthCheckOptions
                {
                    Predicate = _ => false // Use this to differentiate between liveness and readiness checks
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RetailProductMicroservice API V1");
            });

            databaseInitializer.Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}