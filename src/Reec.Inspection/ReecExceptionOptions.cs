using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.Inspection
{
    public class ReecExceptionOptions
    {
        /// <summary>
        /// Lista de claves de cabecera HttpRequest que se debe excluir de la inspección. 
        /// </summary>
        public List<string> HeaderKeysExclude { get; set; } = null;

        /// <summary>
        /// Lista de claves de cabecera HttpRequest que solo se debe incluir de la inspección. 
        /// <para>Ejemplo: Si la cabecera trae 10 keys y la variable HeaderKeysInclude tiene 2 elementos, pues solo 2 key se guardan en BD.</para>
        /// </summary>
        public List<string> HeaderKeysInclude { get; set; } = null;

        /// <summary>
        /// Nombre de la aplicación que registra la excepción.
        /// </summary>
        public string ApplicationName { get; set; } = null;


        /// <summary>
        /// Nombre del esquema de BD
        /// </summary>
        public string Schema { get; set; } = null;


        public string TableName { get; set; } = "LogHttp";

    }

}
