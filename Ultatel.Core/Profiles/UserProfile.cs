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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName));
            CreateMap<StudentCreateDto, Student>();
            _ = CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")).ReverseMap()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => SplitName(src.Name, 0)))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => SplitName(src.Name, 1)));
            CreateMap<Student, StudentSearchDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));


        }
        private string SplitName(string fullName, int partIndex)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return string.Empty;
            }

            string[] parts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (partIndex < 0 || partIndex >= parts.Length)
            {
                return string.Empty;
            }

            return parts[partIndex];
        }
    }
}