﻿using AutoMapper;
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
    }
  }
}