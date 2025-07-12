using App.Business.Helpers;
using App.Business.Services.ExternalServices.Interfaces;
using App.Shared.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Google.Protobuf.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.ExternalServices.Abstractions
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ICloudService _cloudService;

        public FileManagerService(IWebHostEnvironment environment, ICloudService cloudService)
        {
            _environment = environment;
            _cloudService = cloudService;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string fileName = await _cloudService.UploadToCloudAsync(file);

            return fileName;
        }

        public async Task RemoveFileAsync(string fileUrl)
        {
            await _cloudService.DeleteFileFromCloudAsync(fileUrl);
        }

        private async Task<string> UploadLocalAsync(IFormFile file)
        {
            if (!FileChecker.BeAValidImage(file))
                throw new Exception("Invalid file format. Only image, PDF, Word, or PowerPoint files are allowed (maximum size: 20MB).");

            var fileName = Guid.NewGuid().ToString() + "_" +
                Path.GetFileNameWithoutExtension(file.FileName) +
                Path.GetExtension(file.FileName);

            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
