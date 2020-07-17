using School_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.DTOs
{
  /// <summary>
  /// Dto for student dashboard
  /// </summary>
  public class StudentForDetailedDto
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
    /// a list of courses offered by the student
    /// </summary>
    public ICollection<Course> courses { get; set; }

  }
}
