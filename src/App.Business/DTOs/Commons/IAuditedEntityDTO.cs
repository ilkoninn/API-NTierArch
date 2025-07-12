using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.DTOs.Commons
{
    public interface IAuditedEntityDTO
    {
        public IFormFile File { get; set; }
    }
}
