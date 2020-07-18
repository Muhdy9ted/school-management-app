using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.DTOs
{
  /// <summary>
  /// 
  /// </summary>
  public class CourseForCreateDto
  {
    /// <summary>
    /// the title of the course
    /// </summary>
    public string Name { get; set; }


    /// <summary>
    /// the credit unit on the course 
    /// </summary>
    public int creditUnit { get; set; }


    /// <summary>
    /// the faculty that owns the course
    /// </summary>
    public string faculty { get; set; }
  }
}
