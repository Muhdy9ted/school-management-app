using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_App.Helpers
{
  /// <summary>
  /// an extension class
  /// </summary>
  public static class Extensions
  {

    /// <summary>
    /// extending exception handler to get server error messages 
    /// </summary>
    /// <param name="response"></param>
    /// <param name="message"></param>
    public static void AddApplicationError(this HttpResponse response, string message)
    {
      response.Headers.Add("Application-Error", message);
      response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
      response.Headers.Add("Access-Control-Allow-Origin", "*");
    }
  }
}
