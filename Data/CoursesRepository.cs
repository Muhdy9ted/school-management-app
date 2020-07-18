using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_App.Models;

namespace School_Management_App.Data
{
  /// <summary>
  /// courses repository
  /// </summary>
  public class CoursesRepository : ICoursesRepository
  {
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// repo constructor
    /// </summary>
    /// <param name="context"></param>
    public CoursesRepository(ApplicationDbContext context)
    {
      _context = context;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Course>> GetCourses()
    {
      var courses = await _context.Courses.ToListAsync();
      return courses;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Course> GetCourse(int id)
    {
      var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
      return course;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<bool> CourseExist(string name)
    {
      if (await _context.Courses.AnyAsync(x => x.Name == name))
      {
        return true;
      }

      return false;
    }
  }
}