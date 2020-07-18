using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_App.Data;
using School_Management_App.DTOs;
namespace School_Management_App.Controllers
{
  /// <summary>
  /// student controller for student operations
  /// </summary>
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private readonly IStudentRepository _repo;
    private readonly IMapper _mapper;


    /// <summary>
    /// student controller constructor
    /// </summary>
    public StudentsController(IStudentRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }


    /// <summary>
    /// API endpoint for retrieving all registered students
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
      var students = await _repo.GetStudents();
      var studentsToReturn = _mapper.Map<IEnumerable<StudentForListDto>>(students);
      return Ok(studentsToReturn);
    }


    /// <summary>
    /// API endpoint for retrieving a particular student
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetStudent")]
    public async Task<IActionResult> GetStudent(int id)
    {
      var student = await _repo.GetStudent(id);
      var studentToReturn = _mapper.Map<StudentForDetailedDto>(student);
      return Ok(studentToReturn);
    }

    /// <summary>
    /// API endpoint for edit student profile
    /// </summary>
    /// <param name="id"></param>
    /// <param name="studentForUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, StudentForUpdateDto studentForUpdateDto)
    {
      if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
      {
        return Unauthorized();
      }
      var studentFromRepo = await _repo.GetStudent(id);

      _mapper.Map(studentForUpdateDto, studentFromRepo);
      

      if (await _repo.SaveAll())
      {
        return NoContent();
      }
      throw new Exception($"Updating user {id} failed on save");
    }
  }
  
}
