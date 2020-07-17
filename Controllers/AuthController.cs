using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_App.Data;
using School_Management_App.DTOs;
using School_Management_App.Models;

namespace School_Management_App.Controllers
{
  /// <summary>
  /// authentication controller for the student access authentication
  /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
      private readonly IAuthRepository _repo;
      private readonly IMapper _mapper;


      /// <summary>
      /// authcontroller constructor
      /// </summary>
      /// <param name="repo"></param>
      /// <param name="mapper"></param>
      public AuthController(IAuthRepository repo, IMapper mapper)
      {
          _repo = repo;
          _mapper = mapper;
      }

      /// <summary>
      /// API endpoint for student registration
      /// </summary>
      /// <param name="studentForRegisterDto"></param>
      /// <returns></returns>
      [HttpPost("register")]
      public async Task<IActionResult> Register(StudentForRegisterDto studentForRegisterDto)
      {
        studentForRegisterDto.matNo = studentForRegisterDto.matNo.ToLower();
        if (await _repo.StudentExist(studentForRegisterDto.matNo))
        {
          return BadRequest("This Matriculation number already exist, it can't be re-assigned");
        }
        var studentToCreate = _mapper.Map<Student>(studentForRegisterDto);
        var createStudent = await _repo.Register(studentToCreate, studentForRegisterDto.password);
        var studentToReturn = _mapper.Map<StudentForDetailedDto>(createStudent);
        return Ok(studentToReturn);

        /*return CreatedAtRoute("GetStudent", new { controller = "Student", id = createStudent.Id }, studentToReturn);*/ // GetStudent is the name of the method to get a single student method in studentController
      }
    }
}