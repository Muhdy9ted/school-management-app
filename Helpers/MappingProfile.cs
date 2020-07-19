using AutoMapper;
using School_Management_App.DTOs;
using School_Management_App.Models;

namespace School_Management_App
{
  internal class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<StudentForRegisterDto, Student>();
      CreateMap<Student, StudentForDetailedDto>();
      CreateMap<Student, StudentForListDto>();
      CreateMap<StudentForUpdateDto, Student>();
      CreateMap<CourseForCreateDto, Course>();
      CreateMap<CourseForRegistrationDto, Student>().ForMember(dest => dest.courses, opt =>
       {
         opt.MapFrom(src => src.Id);
       });
    }
  }
}