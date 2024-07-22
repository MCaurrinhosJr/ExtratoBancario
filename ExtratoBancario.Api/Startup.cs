using ExtratoBancario.Core.Interfaces.Repositories;
using ExtratoBancario.Core.Interfaces.Services;
using ExtratoBancario.Core.Services;
using ExtratoBancario.Infra.Context;
using ExtratoBancario.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ExtratoBancario.Api
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
            services.AddControllers();
            
            var connectionString = Configuration.GetConnectionString("Banco");

            services.AddDbContext<EBDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "ExtratoBancario API", 
                    Version = "v1", 
                    Description = "Lista de Endpoints da Api" 
                });

                // Adicionar comentários XML ao Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Habilitar o middleware Swagger e Swagger UI no ambiente de desenvolvimento
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExtratoBancario API v1");
            });


            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Configurar rota para a raiz do aplicativo
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Instalacao}/{id?}");

                endpoints.MapControllers();
            });

            StartMigration(app);
        }

        private static void StartMigration(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<EBDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}