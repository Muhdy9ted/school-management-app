using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.Models
{
  /// <summary>
  /// the student model
  /// </summary>
  public class Student
  {
    /// <summary>
    /// the primary key of the table
    /// </summary>
    public int Id { get; set; }


    /// <summary>
    /// student's firstname
    /// </summary>
    public string firstname { get; set; }


    /// <summary>
    /// student's lastname
    /// </summary>
    public string lastname { get; set; }


    /// <summary>
    /// the student's matriculation number
    /// </summary>
    public string matNo { get; set; }


    /// <summary>
    /// storing the student's password in hashed format
    /// </summary>
    public byte[] passwordHash { get; set; }


    /// <summary>
    /// the salt key for hashing the password
    /// </summary>
    public byte[] passwordSalt { get; set; }


    /// <summary>
    /// the student's current level/year 
    /// </summary>
    public int level { get; set; }


    /// <summary>
    /// the student's department
    /// </summary>
    public string department { get; set; }


    /// <summary>
    /// the student's faculty
    /// </summary>
    public string faculty { get; set; }


    /// <summary>
    /// The student's gender
    /// </summary>
    public string gender { get; set; }


    /// <summary>
    /// The date the student created his/her account
    /// </summary>
    public DateTime createdAt { get; set; }


    /// <summary>
    /// a list of courses offered by the student
    /// </summary>
    public ICollection<Course> courses { get; set; }

  }
}
