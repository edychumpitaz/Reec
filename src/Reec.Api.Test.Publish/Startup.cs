using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reec.Inspection;
using Reec.Inspection.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reec.Api.Test.Publish
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

            services.AddReecException<DbContextSqlServer>(options =>
                           options.UseSqlServer("cadena de conexión"));

            // var local = "Data Source=.;Initial Catalog=prueba;Integrated Security=True";
            var local = @"Data Source=ASUS\SQL2019;Initial Catalog=prueba;Integrated Security=True";
            var dev = "data source=172.17.135.17;initial catalog=Dev_ActiveBreak;user id=DEV_ACTIVEBREAK_DBO;password=7Q2WOZJv!#REUdc";


            services.AddReecException<DbContextSqlServer>(options =>
                            options.UseSqlServer(local), new ReecExceptionOptions
                            {   
                                ApplicationName = "ActiveBreak",
                                HeaderKeysExclude = new List<string> { "Postman-Token", "User-Agent" }
                            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseReecException<DbContextSqlServer>();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
