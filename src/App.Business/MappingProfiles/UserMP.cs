using App.Business.DTOs.UserDTOs;
using App.Core.Entities.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class UserMP : Profile
    {
        public UserMP()
        {
            CreateMap<User, UserDTO>()
       .ForMember(dest => dest.IsBanned, opt =>
           opt.MapFrom(src => src.LockoutEnd != null && src.LockoutEnd > DateTimeOffset.UtcNow));
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
        }
    }
}
