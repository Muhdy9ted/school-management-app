using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.DTOs
{
  /// <summary>
  /// 
  /// </summary>
  public class CourseForRegistrationDto
  {
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// the title of the course
    /// </summary>
    public string Name { get; set; }


    /// <summary>
    /// maps to the creditUnnit field of the course model
    /// </summary>
    public int creditUnit { get; set; }


    /// <summary>
    /// maps to the faculty field of the course model
    /// </summary>
    public string faculty { get; set; }
  }
}
