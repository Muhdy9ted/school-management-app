using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.DTOs
{
  /// <summary>
  /// dto for student login
  /// </summary>
  public class StudentForLoginDto
  {
    /// <summary>
    /// maps to the matNo field in the student entity
    /// </summary>
    public string matNo { get; set; }


    /// <summary>
    /// stores the student's password which is then used in generating the passwordhash field which is then validated against the stored passwordHarsh
    /// </summary>
    public string password { get; set; }
  }
}
