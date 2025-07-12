using App.Business.DTOs.SettingDTOs;
using App.Business.Services.InternalServices.Interfaces;
using App.Core.Entities;
using App.DAL.Repositories.Implementations;
using App.DAL.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Abstractions
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;

        public SettingService(ISettingRepository settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        public async Task<SettingDTO> AddAsync(CreateSettingDTO dto)
        {
            var entity = _mapper.Map<Setting>(dto);
            var result = await _settingRepository.AddAsync(entity);

            return _mapper.Map<SettingDTO>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _settingRepository.GetByIdAsync(x => x.Id == id);
            await _settingRepository.DeleteAsync(entity);
        }

        public async Task<IQueryable<SettingDTO>> GetAllAsync()
        {
            return (await _settingRepository.GetAllAsync(x => true, false))
                .Select(x => _mapper.Map<SettingDTO>(x))
                .AsQueryable();
        }

        public async Task RecoverAsync(int id)
        {
            var entity = await _settingRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted);
            await _settingRepository.RecoverAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _settingRepository.GetByIdAsync(x => x.Id == id);
            await _settingRepository.RemoveAsync(entity);
        }

        public async Task<SettingDTO> UpdateAsync(UpdateSettingDTO dto)
        {
            var entity = await _settingRepository.GetByIdAsync(x => x.Id == dto.Id);
            var updatedEntity = _mapper.Map(dto, entity);
            var result = await _settingRepository.UpdateAsync(updatedEntity);

            return _mapper.Map<SettingDTO>(result);
        }
    }
}
