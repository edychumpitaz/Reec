using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.Inspection
{
    public static class ServiceCollectionsExtensions
    {

        /// <summary>
        /// Agregamos el servicio de captura de errores personalizado, para que se guarde en Base de datos de forma automática.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddReecException(this IServiceCollection services)
        {
            
            return services;
        }

    }

}
