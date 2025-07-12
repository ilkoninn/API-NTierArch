using App.Business.DTOs.SettingDTOs;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class SettingMP : Profile
    {
        public SettingMP()
        {
            CreateMap<Setting, SettingDTO>().ReverseMap();
            CreateMap<CreateSettingDTO, Setting>();
            CreateMap<UpdateSettingDTO, Setting>();
        }
    }
}
