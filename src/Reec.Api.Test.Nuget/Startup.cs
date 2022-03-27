using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reec.Api.Test.Nuget.DbLegacyTest;
using Reec.Inspection;
using Reec.Inspection.SqlServer;
//using Reec.Inspection;
//using Reec.Inspection.SqlServer;

namespace Reec.Api.Test.Nuget
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
            services.AddControllers();

            var reec = @"Data Source=.\SQL2019;Initial Catalog=prueba;Integrated Security=True";
            var dev = @"Data Source=.\SQL2019;Initial Catalog=pruebaDEV;Integrated Security=True";




            services.AddDbContext<TestDbContext>(options =>
                            options.UseSqlServer(dev));

            services.AddReecException<DbContextSqlServer>(options =>
                          options.UseSqlServer(reec), new ReecExceptionOptions
                          {
                              EnableMigrations = false
                          });




            //Ejemplo de migracion con ruta de namespace(MigrationsAssembly)
            //services.AddReecException<DbContextSqlServer>(options =>
            //          options.UseSqlServer(local, x => x.MigrationsAssembly("Reec.Inspection.SqlServer")));


            //Mas formas de personalizar
            //services.AddReecExceptionOptions<DbContextSqlServer>(options =>
            //          options.UseSqlServer(local, x => x.MigrationsAssembly("Reec.Inspection.SqlServer")),
            //          new ReecExceptionOptions
            //          {
            //              HeaderKeysExclude = new List<string> { "Postman-Token", "User-Agent" }
            //          });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseReecException<DbContextSqlServer>();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
