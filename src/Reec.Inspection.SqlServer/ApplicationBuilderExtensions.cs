using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.Inspection.SqlServer
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseReecExceptionMiddleware<TDbContext>(this IApplicationBuilder applicationBuilder) where TDbContext : InspectionDbContext
        {
            
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            var isExists = context.Database.EnsureCreated();
            context.Database.Migrate();

            applicationBuilder.UseMiddleware<ReecExceptionMiddleware<TDbContext>>();

            return applicationBuilder;
        }


    }




}
