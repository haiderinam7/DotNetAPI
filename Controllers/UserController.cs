using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    public UserController(IConfiguration config)
    {
        Console.WriteLine(config.GetConnectionString("DefaultConnection"));
    }

    [HttpGet("GetUsers/{testValue}")]

    public string[] GetUsers(string testValue)
    {
            string[] responseArray = new string[] {
                "test1",
                "test2",
                testValue
            };
            return responseArray;
    }
}