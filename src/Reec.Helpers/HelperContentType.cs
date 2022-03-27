using Microsoft.AspNetCore.StaticFiles;

namespace Reec.Helpers
{

    /// <summary>
    /// Helper para obtener ContentType de archivos.
    /// </summary>
    public static class HelperContentType
    {

        private static readonly FileExtensionContentTypeProvider FileContentType = new FileExtensionContentTypeProvider();

        /// <summary>
        /// Obtener ContentType apartir del nombre de un archivo.
        /// </summary>
        /// <param name="FileName">El nombre de archivo debe incluir la extensión.
        /// <para>Ejemplo:  test.pdf</para>
        /// </param>
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
