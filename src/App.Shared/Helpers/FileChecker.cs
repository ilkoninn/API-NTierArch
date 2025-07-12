using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Helpers
{
    public static class FileChecker
    {
        public static bool BeAValidImage(IFormFile file)
        {
            return file != null
                && IsAllowedContentType(file.ContentType)
                && file.Length <= 20 * 1024 * 1024; // 20 MB
        }

        private static bool IsAllowedContentType(string contentType)
        {
            string[] allowedTypes = new[]
            {
                "application/pdf",
                "application/msword",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "application/vnd.ms-powerpoint",
                "application/vnd.openxmlformats-officedocument.presentationml.presentation",                "image/jpeg",
                "image/png",
                "image/gif",
                "image/webp"
            };

            return allowedTypes.Contains(contentType);
        }
    }
}
