using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.Helpers
{
    public static class HelperContentType
    {

        private static readonly FileExtensionContentTypeProvider FileContentType = new FileExtensionContentTypeProvider();
        public static string GetContentType(string FileName)
        {

            var vResult = FileContentType.TryGetContentType(FileName, out string contentType);
            if (!vResult) 
                contentType = "application/octet-stream";
            
            return contentType;
        }


    }

}
