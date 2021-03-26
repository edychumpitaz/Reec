using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.Helpers
{

    /// <summary>
    /// Ayuda pata ContentType de archivos.
    /// </summary>
    public static class HelperContentType
    {

        private static readonly FileExtensionContentTypeProvider FileContentType = new FileExtensionContentTypeProvider();

        /// <summary>
        /// Obtener ContentType apartir del nombre de un archivo.
        /// </summary>
        /// <param name="FileName">Nombre de archivo</param>
        /// <returns></returns>
        public static string GetContentType(string FileName)
        {

            var vResult = FileContentType.TryGetContentType(FileName, out string contentType);
            if (!vResult) 
                contentType = "application/octet-stream";
            
            return contentType;
        }


    }

}
