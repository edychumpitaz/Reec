using System;
using System.Collections.Generic;
using System.Net;
using static Reec.Inspection.ReecEnums;

namespace Reec.Inspection
{
    public class ReecMessage
    {

        public ReecMessage(Category category, string message, string path = null)
        {

            this.Message = new List<string>() { message };
            this.Category = category;
            this.Path = path;
        }

        public ReecMessage(Category category, List<string> message, string path = null)
        {
            this.Message = message;
            this.Category = category;
            this.Path = path;
        }


        /// <summary>
        /// Código de error registrado en el sistema, si es cero es informativo.
        /// </summary>
        public int Id { get; set; }       

        /// <summary>
        /// Path url de consulta que originó el mensaje
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Identificación del HttpRequest para seguimiento de log.
        /// </summary>
        public string TraceIdentifier { get; set; }


        /// <summary>
        /// Categoria de mensaje de donde se origina el error o la información.
        /// </summary>
        public Category Category { get; set; }

        public string CategoryDescription
        {
            get
            {
                return Category.ToString();
            }
        }

        public List<string> Message { get; set; }

    }

}
