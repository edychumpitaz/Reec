using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reec.Inspection;
using Reec.Inspection.SqlServer;

namespace Reec.Api.Test
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

            //var local = "Data Source=.;Initial Catalog=prueba;Integrated Security=True";
            var local = @"Data Source=ASUS\SQL2019;Initial Catalog=prueba;Integrated Security=True";
            //var dev = "data source=172.17.135.17;initial catalog=Dev_ActiveBreak;user id=DEV_ACTIVEBREAK_DBO;password=7Q2WOZJv!#REUdc";



            services.AddReecException<DbContextSqlServer>(options =>
                            options.UseSqlServer(local));


            //services.AddReecException<DbContextSqlServer>(options =>
            //options.UseSqlServer(local,
            //x => x.MigrationsAssembly(typeof(DbContextSqlServer).Assembly.FullName)));



            //Ejemplo de migracion con ruta de namespace(MigrationsAssembly)
            //services.AddReecException<DbContextSqlServer>(options =>
            //          options.UseSqlServer(local, x => x.MigrationsAssembly("Reec.Inspection.SqlServer")));


            //Mas formas de personalizar
            //services.AddReecException<DbContextSqlServer>(options =>
            //          options.UseSqlServer(local, x => x.MigrationsAssembly("Reec.Inspection.SqlServer")),
            //          new ReecExceptionOptions
            //          {
            //              HeaderKeysExclude = new List<string> { "Postman-Token", "User-Agent" }
            //          });




            //services.AddDbContext<DbContextSqlServer>(options =>
            //    options.UseSqlServer(local),
            //    ServiceLifetime.Transient, ServiceLifetime.Transient);

            //services.AddDbContext<DbContextSqlServer>(options =>
            //    options.UseSqlServer(local, x => x.MigrationsAssembly(typeof(Startup).Assembly.FullName)),
            //    ServiceLifetime.Transient, ServiceLifetime.Transient);
            //services.AddDbContext<DbContextSqlServer>(options =>
            //   options.UseSqlServer(local, x => x.MigrationsAssembly("Reec.Inspection.SqlServer")),
            //   ServiceLifetime.Transient, ServiceLifetime.Transient);

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
