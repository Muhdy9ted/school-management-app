using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.DTOs
{
  /// <summary>
  /// 
  /// </summary>
  public class StudentForListDto
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


  }
}
