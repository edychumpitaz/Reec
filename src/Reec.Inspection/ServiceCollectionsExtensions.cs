using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Reec.Inspection
{
    public static class ServiceCollectionsExtensions
    {

        /// <summary>
        /// Agregamos servicio de control de errores automáticos.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction">Agregamos configuración de base de datos</param>
        /// <param name="exceptionOptions">Opciones de filtros personalizados.</param>
        /// <returns></returns>
        public static IServiceCollection AddReecException<TDbContext>(this IServiceCollection services,
                        [NotNull] Action<DbContextOptionsBuilder> optionsAction,
                         ReecExceptionOptions exceptionOptions = null) where TDbContext : InspectionDbContext
        {

            if (exceptionOptions == null)
                services.AddTransient<ReecExceptionOptions, ReecExceptionOptions>();
            else
                services.AddTransient(opt => exceptionOptions);


            services.AddDbContext<TDbContext>(optionsAction,
               ServiceLifetime.Transient, ServiceLifetime.Transient);

            return services;
        }




        ///// <summary>
        ///// Agregamos servicio de control de errores automáticos.
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="optionsAction">Agregamos configuración de base de datos</param>
        ///// <param name="exceptionOptions">Opciones de filtros personalizados.</param>
        ///// <returns></returns>
        //public static IServiceCollection AddReecException<TDbContext>(this IServiceCollection services,
        //                [NotNull] Action<DbContextOptionsBuilder> optionsAction,
        //                Func<IServiceCollection, ReecExceptionOptions> exceptionOptions = null) where TDbContext : InspectionDbContext
        //{

        //    if (exceptionOptions == null)
        //        services.AddTransient<ReecExceptionOptions, ReecExceptionOptions>();
        //    else
        //        services.AddTransient(opt => exceptionOptions);


        //    var nuevo = exceptionOptions.Invoke();

        //    services.AddDbContext<TDbContext>(optionsAction,
        //       ServiceLifetime.Transient, ServiceLifetime.Transient);

        //    return services;
        //}




    }

}
