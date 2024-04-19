using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration configuration)
    {
        _dapper = new DataContextDapper(configuration);
    }
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GEGTDATE()");
    }

    [HttpGet("GetUsers/{testValue}")]
    // public IActionResult Test()
    public string[] GetUsers(string testValue)
    {
        string[] responseArray = new string[]{
            testValue
        };
        return responseArray;
    }
}