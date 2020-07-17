using Microsoft.EntityFrameworkCore;
using School_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.Data
{
  /// <summary>
  /// the context session that interfaces with our database to provide us several operations and functionalities
  /// </summary>
  public class ApplicationDbContext : DbContext
  {

    /// <summary>
    /// accessing the instance of the DbContext options
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    /// <summary>
    /// course model entity
    /// </summary>
    public DbSet<Course> Courses { get; set; }


    /// <summary>
    /// student model entity
    /// </summary>
    public DbSet<Student> Students { get; set; }
  }
}
