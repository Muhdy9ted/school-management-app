using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.DTOs
{
  /// <summary>
  /// 
  /// </summary>
  public class StudentForUpdateDto
  {
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
  }
}
