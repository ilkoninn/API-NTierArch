using App.Business.DTOs.UserDTOs;
using App.Business.Services.InternalServices.Abstractions;
using App.Business.Services.InternalServices.Interfaces;
using App.Business.Validators.UserValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var result = _userService.GetAllAsync();

            return Ok(result);
        }

        [HttpPatch("ban/{id}")]
        public async Task<IActionResult> LockedOutAsync(string id)
        {
            await _userService.LockedOutAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(string id)
        {
            await _userService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveAsync(string id)
        {
            await _userService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDTO dto)
        {
            var validationResult = await new CreateUserDTOValidator().ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDTO dto)
        {
            var validationResult = await new UpdateUserDTOValidator().ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.UpdateAsync(dto);
            return Ok(result);
        }

    }
}
