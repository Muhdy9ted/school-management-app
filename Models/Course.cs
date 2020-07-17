using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.Models
{
  /// <summary>
  /// The course model 
  /// </summary>
  public class Course
  {
    /// <summary>
    /// the primary key of the course model
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// the title of the course
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// the credit unit on the course 
    /// </summary>
    public int creditUnit { get; set; }
  }
}
