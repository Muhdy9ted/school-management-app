using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
    private readonly IConfiguration _config;


    /// <summary>
    /// authcontroller constructor
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>
    /// <param name="config"></param>
    public AuthController(IAuthRepository repo, IMapper mapper, IConfiguration config)
    {
      _repo = repo;
      _mapper = mapper;
      _config = config;
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


    /// <summary>
    /// API endpoint for student login
    /// </summary>
    /// <param name="studentForLoginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(StudentForLoginDto studentForLoginDto)
    {
      var studentFromRepo = await _repo.Login(studentForLoginDto.matNo.ToLower(), studentForLoginDto.password);
      if (studentFromRepo == null)
      {
        return Unauthorized();
      }
      var claims = new[]
      {
          new Claim(ClaimTypes.NameIdentifier, studentFromRepo.Id.ToString()),
          new Claim(ClaimTypes.SerialNumber, studentFromRepo.matNo),
          new Claim(ClaimTypes.Name, studentFromRepo.firstname)
        };
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(3),
        SigningCredentials = creds
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Ok(new { token = tokenHandler.WriteToken(token) });
    }
  }
}