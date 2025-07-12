using App.Business.DTOs.UserDTOs;
using App.Business.Services.InternalServices.Interfaces;
using App.Core.Entities.Identity;
using App.Core.Enums;
using App.Core.Exceptions.Commons;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Abstractions
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDTO> AddAsync(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);

            user.Email = $"{dto.FullName.ToLower()}@devservice.az".Replace(" ", "_");
            user.UserName = dto.FullName.ToLower().Replace(" ", "_");
            user.EmailConfirmed = true;

            EnsureIdentitySucceeded(await _userManager.CreateAsync(user, $"{dto.FullName.Replace(" ", "_")}dev123!@"));
            EnsureIdentitySucceeded(await _userManager.AddToRoleAsync(user, EUserRole.Employee.ToString()));

            return _mapper.Map<UserDTO>(user);
        }

        public async Task LockedOutAsync(string id)
        {
            var user = await CheckUserNotFoundAsync(id);

            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            EnsureIdentitySucceeded(await _userManager.UpdateAsync(user));
        }

        public IQueryable<UserDTO> GetAllAsync()
        {
            return _userManager.Users
                .Where(x => x.UserName != "admin")
                .Select(user => _mapper.Map<UserDTO>(user)).AsQueryable();
        }

        public async Task RecoverAsync(string id)
        {
            var user = await CheckUserNotFoundAsync(id);

            user.LockoutEnd = null;
            EnsureIdentitySucceeded(await _userManager.UpdateAsync(user));
        }

        public async Task RemoveAsync(string id)
        {
            EnsureIdentitySucceeded(await _userManager.DeleteAsync(await CheckUserNotFoundAsync(id)));
        }

        public async Task<UserDTO> UpdateAsync(UpdateUserDTO dto)
        {
            var oldUser = await CheckUserNotFoundAsync(dto.Id);
            var updatedUser = _mapper.Map(dto, oldUser);

            updatedUser.Email = $"{dto.FullName.ToLower()}@devservice.az".Replace(" ", "_");    
            updatedUser.UserName = dto.FullName.ToLower().Replace(" ", "_");

            if (dto.Password is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(updatedUser);
                EnsureIdentitySucceeded(await _userManager.
                    ResetPasswordAsync(updatedUser, token, dto.Password));
            }

            EnsureIdentitySucceeded(await _userManager.UpdateAsync(updatedUser));

            return _mapper.Map<UserDTO>(updatedUser);
        }

        private async Task<User> CheckUserNotFoundAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                throw new EntityNotFoundException($"Entity of type {typeof(User).Name.ToLower()} not found.");

            return user;
        }

        private void EnsureIdentitySucceeded(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"{errors}");
            }
        }

    }
}
