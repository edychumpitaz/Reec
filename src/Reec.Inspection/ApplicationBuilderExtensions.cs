﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Reec.Inspection
{
    public static class ApplicationBuilderExtensions
    {

        /// <summary>
        /// Middleware encargado de interceptar el HttpRequest y de capturar los errores generados para guardar en base de datos.
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseReecException<TDbContext>(this IApplicationBuilder applicationBuilder) where TDbContext : InspectionDbContext
        {

            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            var options = scope.ServiceProvider.GetRequiredService<ReecExceptionOptions>();
            //var isExists = context.Database.EnsureCreated();

            if (options.EnableMigrations)
                context.Database.Migrate();

            applicationBuilder.UseMiddleware<ReecExceptionMiddleware<TDbContext>>();

            return applicationBuilder;
        }


    }




}
