using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.DTOs
{
  /// <summary>
  /// Dto for student registration
  /// </summary>
  public class StudentForRegisterDto
  {
    /// <summary>
    /// maps to the matNo field of the student entity
    /// </summary>
    [Required]
    [StringLength(12, ErrorMessage = "matriculation number cannot be longer than 12 characters")]
    public string matNo { get; set; }



    /// <summary>
    /// stores the student's password which is then used in generating the passwordhash field in the student entity
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "You must specify a password of minimum 8 characters")]
    public string password { get; set; }



    /// <summary>
    /// maps to the firstname field of the student entity
    /// </summary>
    [Required]
    [StringLength(30)]
    public string firstname { get; set; }



    /// <summary>
    /// maps to the lastname field of the student entity
    /// </summary>
    /// <returns></returns>
    [Required]
    [StringLength(30)]
    public string lastname { get; set; }



    /// <summary>
    /// maps to the level field of the student entity
    /// </summary>
    [Required]
    public int level { get; set; }



    /// <summary>
    /// maps to the department field of the student entity
    /// </summary>
    [Required]
    [StringLength(30)]
    public string department { get; set; }



    /// <summary>
    /// maps to the faculty field of the student entity
    /// </summary>
    /// <returns></returns>
    [Required]
    [StringLength(30)]
    public string faculty { get; set; }



    /// <summary>
    /// maps to the gender field of the student entity
    /// </summary>
    [Required]
    [StringLength(30)]
    public string gender { get; set; }


    /// <summary>
    /// maps to the dateJoined field of the student entity
    /// </summary>
    public DateTime createdAt { get; set; }


    /// <summary>
    /// class constructor
    /// </summary>
    public StudentForRegisterDto()
    {
      createdAt = DateTime.Now;
    }
  }
}
