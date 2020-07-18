using School_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.Data
{
  /// <summary>
  /// course interface for repo pattern
  /// </summary>
  public interface ICoursesRepository
  {
    /// <summary>
    /// for adding a courses
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Add<T>(T entity) where T : class;


    /// <summary>
    /// for deleting
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Delete<T>(T entity) where T : class;


    /// <summary>
    /// for saving changes
    /// </summary>
    /// <returns></returns>
    Task<bool> SaveAll();


    /// <summary>
    /// for returning a list of students
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Course>> GetCourses();


    /// <summary>
    /// for getting a particular student
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Course> GetCourse(int id);


    /// <summary>
    /// verify if course exist method for the auth controller
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> CourseExist(string name);
  }
}
