using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Dtos;
using Ultatel.Core.Entities;

namespace Ultatel.Core.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest=>dest.UserName,opt=>opt.MapFrom(src=>src.FullName));
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentDto>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<Student, StudentSearchDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
                 

        }
    }
}
