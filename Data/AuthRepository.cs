using Microsoft.EntityFrameworkCore;
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
  public class AuthRepository : IAuthRepository
  {
    private readonly ApplicationDbContext _context;


    /// <summary>
    /// AuthRepo constructor
    /// </summary>
    /// <param name="context"></param>
    public AuthRepository(ApplicationDbContext context)
    {
      _context = context;
    }


    /// <summary>
    /// endpoint for student login
    /// </summary>
    /// <param name="matNo"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<Student> Login(string matNo, string password)
    {
      var student = await _context.Students.FirstOrDefaultAsync(x => x.matNo == matNo);

      if (student == null)
      {
        return null;
      }

      if (!(VerifyPasswordHash(password, student.passwordHash, student.passwordSalt)))
      {
        return null;
      }

      // Auth succesfull
      return student;
    }


    /// <summary>
    /// verify if student's password matches with whats in the db 
    /// </summary>
    /// <returns></returns>
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i])
            return false;
        }
      }
      return true;
    }


    /// <summary>
    /// endpoint for student registration
    /// </summary>
    /// <param name="student"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<Student> Register(Student student, string password)
    {
      byte[] passwordHash, passwordSalt;

      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      student.passwordHash = passwordHash;
      student.passwordSalt = passwordSalt;

      await _context.Students.AddAsync(student);
      await _context.SaveChangesAsync();

      return student;
    }
  

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }


    /// <summary>
    /// endpoint to verify if a student with a particular matNo already exist 
    /// </summary>
    /// <param name="matNo"></param>
    /// <returns></returns>
    public async Task<bool> StudentExist(string matNo)
    {
      if (await _context.Students.AnyAsync(x => x.matNo == matNo))
      {
        return true;
      }

      return false;
    }
  }
}
