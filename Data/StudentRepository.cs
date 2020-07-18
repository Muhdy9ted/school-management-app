using Microsoft.EntityFrameworkCore;
using School_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.Data
{
  /// <summary>
  /// the student controller repo
  /// </summary>
  public class StudentRepository : IStudentRepository
  {
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// the student controller repo constructor
    /// </summary>
    /// <param name="context"></param>
    public StudentRepository(ApplicationDbContext context)
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
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Student> GetStudent(int id)
    {
      var student = await _context.Students.Include(x => x.courses).FirstOrDefaultAsync(u => u.Id == id);
      return student;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Student>> GetStudents()
    {
      var students = await _context.Students.ToListAsync();
      return students;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}
