using App.Business.DTOs.SettingDTOs;
using App.Business.Services.InternalServices.Interfaces;
using App.Business.Validators.SettingValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _settingService.GetAllAsync();

            return Ok(result);
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _settingService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(int id)
        {
            await _settingService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _settingService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSettingDTO dto)
        {
            var validationResult = await new CreateSettingDTOValidator().ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _settingService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateSettingDTO dto)
        {
            var validationResult = await new UpdateSettingDTOValidator().ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _settingService.UpdateAsync(dto);
            return Ok(result);
        }
    }
}
