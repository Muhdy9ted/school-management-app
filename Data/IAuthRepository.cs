using School_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.Data
{
  /// <summary>
  /// implementing the Repository pattern for AuthController. 
  /// </summary>
  public interface IAuthRepository
  {
    /// <summary>
    /// register method for the auth controller
    /// </summary>
    /// <param name="student"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<Student> Register(Student student, string password);


    /// <summary>
    /// login method for the auth controller
    /// </summary>
    /// <param name="matNo"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<Student> Login(string matNo, string password);


    /// <summary>
    /// verify student exist method for the auth controller
    /// </summary>
    /// <param name="matNo"></param>
    /// <returns></returns>
    Task<bool> StudentExist(string matNo);
  }
}
