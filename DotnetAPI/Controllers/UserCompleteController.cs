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
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GEGTDATE()");
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

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET [FirstName] = '" + user.FirstName +
            "',[LastName] = '" + user.LastName +
            "',[Email] = '" + user.Email +
            "',[Gender] = '" + user.Gender +
            "',[Active] = '" + user.Active +
            "' WHERE UserId = " + user.UserId;
        System.Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.Users(
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            ) VALUES(" +
                "'" + user.FirstName +
                "', '" + user.LastName +
                "', '" + user.Email +
                "', '" + user.Gender +
                "', '" + user.Active +
            "')";
        System.Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.Users 
                WHERE UserId = " + userId;
        System.Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Delete User");
    }


    //HTTP endpoinds for UserSalary model

    [HttpPut("UserSalary")]
    public IActionResult PutUserSalary(UserSalary user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.UserSalary
            SET Salary = " + user.Salary +
            " WHERE UserId = " + user.UserId;
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(user);
        }
        throw new Exception("Failed to Update UserSalary");
    }

    [HttpPost("UserSalary")]
    public IActionResult PostUserSalary(UserSalary user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserSalary(
                UserId,
                Salary
            ) VALUES(" + user.UserId
            + ", " + user.Salary +
                ")";
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(user);
        }
        throw new Exception("Failed to Post User Salary");
    }

    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.UserSalary
                WHERE UserId = " + userId;
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Delete User Salary");
    }

    //HTTP endpoinds for UserJobInfo model
    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfo(UserJobInfo userJobInfoForInsert)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserJobInfo (
                UserId,
                Department,
                JobTitle
            ) VALUES (" + userJobInfoForInsert.UserId
                + ", '" + userJobInfoForInsert.Department
                + "', '" + userJobInfoForInsert.JobTitle
                + "')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userJobInfoForInsert);
        }
        throw new Exception("Adding User Job Info failed on save");
    }

    [HttpPut("UserJobInfo")]
    public IActionResult PutUserJobInfo(UserJobInfo userJobInfoForUpdate)
    {
        string sql = "UPDATE TutorialAppSchema.UserJobInfo SET Department='"
            + userJobInfoForUpdate.Department
            + "', JobTitle='"
            + userJobInfoForUpdate.JobTitle
            + "' WHERE UserId=" + userJobInfoForUpdate.UserId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userJobInfoForUpdate);
        }
        throw new Exception("Updating User Job Info failed on save");
    }
    
    [HttpDelete("UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserJobInfo 
                WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }
}