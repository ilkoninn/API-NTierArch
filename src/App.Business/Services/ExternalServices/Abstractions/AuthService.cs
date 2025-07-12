using App.Business.DTOs.AuthDTOs;
using App.Business.Helpers;
using App.Business.Services.ExternalServices.Interfaces;
using App.Core.Entities.Identity;
using App.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.ExternalServices.Abstractions
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IClaimService _claimService;

        public AuthService(
            UserManager<User> userManager, IClaimService claimService, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _claimService = claimService;
            _configuration = configuration;
        }


        public async Task LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);

            if (user == null)
                user = await _userManager.FindByNameAsync(dto.UsernameOrEmail);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedAccessException("Email or password is incorrect.");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "RoleError";

            var token = JwtGenerator.GenerateToken(user, role, _configuration);

            _claimService.CreateAccessToken(token);
        }

        public void LogoutAsync()
        {
            _claimService.RemoveAccessToken();
        }
    }
}
