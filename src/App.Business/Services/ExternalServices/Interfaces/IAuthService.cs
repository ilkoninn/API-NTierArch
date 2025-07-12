using App.Business.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.ExternalServices.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(LoginDTO dto);
        void LogoutAsync();
    }
}
