using App.Business.DTOs.SettingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface ISettingService
    {
        Task<IQueryable<SettingDTO>> GetAllAsync();
        Task RemoveAsync(int id);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task<SettingDTO> AddAsync(CreateSettingDTO dto);
        Task<SettingDTO> UpdateAsync(UpdateSettingDTO dto);
    }
}
