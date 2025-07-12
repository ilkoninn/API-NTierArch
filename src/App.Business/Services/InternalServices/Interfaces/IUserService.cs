using App.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface IUserService
    {
        IQueryable<UserDTO> GetAllAsync();
        Task RemoveAsync(string id);
        Task LockedOutAsync(string id);
        Task RecoverAsync(string id);
        Task<UserDTO> AddAsync(CreateUserDTO dto);
        Task<UserDTO> UpdateAsync(UpdateUserDTO dto);
    }
}
