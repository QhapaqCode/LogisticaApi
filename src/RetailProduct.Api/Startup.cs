using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Application.Services;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Infrastructure.Data;
using RetailProductMicroservice.Infrastructure.Repositories;
using RetailProductMicroservice.Api.Services;

namespace RetailProductMicroservice.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
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
        
            services.AddDbContext<RetailProductContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
        
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
        
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RetailProductMicroservice API", Version = "v1" });
            });
        
            // Registrar DatabaseInitializer
            services.AddSingleton<DatabaseInitializer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseInitializer databaseInitializer)
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