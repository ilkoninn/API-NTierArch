using App.Business.DTOs.Commons;
using App.Business.Services.ExternalServices.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles.Commons
{
    public class CustomMappingAction<TSource, TDestination> : IMappingAction<TSource, TDestination> where TSource : IAuditedEntityDTO
    {
        private readonly IFileManagerService _fileManagerService;

        public CustomMappingAction(IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
        }

        public void Process(TSource source, TDestination destination, ResolutionContext context)
        {
            if (source.File != null)
            {
                var uploadedUrl = _fileManagerService.UploadFileAsync(source.File).Result;
                var property = destination.GetType().GetProperty("FilePath");

                if (property != null && property.PropertyType == typeof(string))
                {
                    property.SetValue(destination, uploadedUrl);
                }
            }
        }
    }
}
