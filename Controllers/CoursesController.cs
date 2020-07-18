using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_App.Data;
using School_Management_App.DTOs;
using School_Management_App.Models;

namespace School_Management_App.Controllers
{
    /// <summary>
    /// the courses controller providing us with various http operations
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesRepository _repo;
        private readonly IMapper _mapper;


      /// <summary>
      /// the courses controller constructor
      /// </summary>
      /// <param name="repo"></param>
      /// <param name="mapper"></param>
      public CoursesController(ICoursesRepository repo, IMapper mapper)
      {
        _repo = repo;
        _mapper = mapper;
      }


        // GET: api/Courses
        /// <summary>
        /// API endpoint for retrieving all the available courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _repo.GetCourses();
            return Ok(courses);
        }



        // GET: api/Courses/5
        /// <summary>
        /// API endpoint for retrieving a particular course 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _repo.GetCourse(id);
            return Ok(course);
        }

  

        // POST: api/Courses
        /// <summary>
        /// API endpoint for creating a course
        /// </summary>
        /// <param name="courseForCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseForCreateDto courseForCreateDto)
        {
          courseForCreateDto.Name = courseForCreateDto.Name.ToLower();
          if (await _repo.CourseExist(courseForCreateDto.Name))
          {
            return BadRequest("This Course alreaady exist, you can edit it to reflect new updates");
          }
          var courseToCreate = _mapper.Map<Course>(courseForCreateDto);
          _repo.Add(courseToCreate);
          if(await _repo.SaveAll())
          {
            return CreatedAtAction("GetCourse", new { id = courseToCreate.Id }, courseToCreate);
          }
          throw new Exception($"creating course {courseToCreate.Name} failed on save");
        }


    }
}
