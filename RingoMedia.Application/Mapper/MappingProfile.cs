using AutoMapper;
using RingoMedia.Dtos;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.Mapper
{
    internal class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentDTO, Department>()
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore());

            CreateMap<Department, DepartmentDTO>()
              .ForMember(dest => dest.SubDepartments, opt => opt.MapFrom(src => src.SubDepartments))
              .ForMember(dest => dest.SubDepartmentIds, opt => opt.MapFrom(src => src.SubDepartments.Select(sd => sd.Id).ToList()))
              .ForMember(dest => dest.ParentDepartmentId, opt => opt.MapFrom(src => src.ParentDepartmentId));

          
           
            CreateMap<Reminder, ReminderDTO>().ReverseMap();
        }
    }
}
