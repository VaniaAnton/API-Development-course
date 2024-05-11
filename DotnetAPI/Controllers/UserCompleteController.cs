using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;
    public UserCompleteController(IConfiguration configuration)
    {
        _dapper = new DataContextDapper(configuration);
    }

    //HTTP endpoinds for User model
    [HttpGet("GetUsers/{userId}/{Active}")]
    // public IActionResult Test()
    public IEnumerable<UserComplete> GetUsers(int userId, bool Active)
    {
        string sql = @"EXEC TutorialAppSchema.spUsers_Get";
        if (userId != 0)
        {
            sql += " @UserId= " + userId.ToString();
        }
        if (Active)
        {
            sql += ", @Active= " + Active.ToString();
        }

        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);
        return users;
    }

    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserComplete user)
    {
        string sql = @" EXEC TutorialAppSchema.spUser_Upsert
            @FirstName = '" + user.FirstName +
            "', @LastName = '" + user.LastName +
            "', @Email = '" + user.Email +
            "', @Gender = '" + user.Gender +
            "', @Active ='" + user.Active +
            "', @JobTitle ='" + user.JobTitle +
            "', @Department ='" + user.Department +
            "', @Salary = " + user.Salary +
            "', @UserId = " + user.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Update User");
    }



    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @" EXEC TutorialAppSchema.spUser_Delete 
                @UserId = " + userId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Delete User");
    }
}