using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("Posts/{postId}/{userId}/{searchParam}")]
        public IEnumerable<Post> GetPosts(int postId = 0, int userId = 0, string searchParam = "None")
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get";
            string parameters = "";
            DynamicParameters sqlParameter = new DynamicParameters();
            if (postId != 0)
            {
                parameters += ", @PostId= @PostIdParameter";
                sqlParameter.Add("@PostIdParameter", postId, DbType.Int32);
            }
            if (userId != 0)
            {
                parameters += ", @UserId= @UserIdParameter";
                sqlParameter.Add("@UserIdParameter", userId, DbType.Int32);
            }
            if (searchParam.ToLower() != "none")
            {
                parameters += ", @SearchValue= @SearchValueParameter'";
                sqlParameter.Add("@SearchValueParameter", searchParam, DbType.String);
            }

            if (parameters.Length > 0)
            {
                sql += parameters.Substring(1);
            }

            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameter);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get @UserId = @UserIdParameter";
            DynamicParameters sqlParameter = new DynamicParameters();
            sqlParameter.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);

            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameter);
        }

        [HttpPut("UpsertPost")]
        public IActionResult UpsertPost(Post postToUpsert)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Upsert
                @UserId = @UserIdParameter,
                @PostTitle = @PostTitleParameter,
                @PostContent = @PostContentParameter";

            DynamicParameters sqlParameter = new DynamicParameters();
            sqlParameter.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameter.Add("@PostTitleParameter", postToUpsert.PostTitle, DbType.String);
            sqlParameter.Add("@PostContentParameter", postToUpsert.PostContent, DbType.String);
            string stringParameter = "";
            if (postToUpsert.PostId > 0)
            {
                stringParameter += ", @PostId = @PostIdParameter";
                sqlParameter.Add("@PostIdParameter", postToUpsert.PostId, DbType.Int32);
            }
            if (stringParameter.Length > 0)
            {
                sql += stringParameter;
            }

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameter))
            {
                return Ok();
            }

            throw new Exception("Failed to upsert post!");
        }


        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"EXEC TutorialAppSchema.spPost_Delete 
                @UserId = @UserIdParameter,
                @PostId = @PostIdParameter";

            DynamicParameters sqlParameter = new DynamicParameters();
            sqlParameter.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameter.Add("@PostIdParameter", postId, DbType.Int32);

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameter))
            {
                return Ok();
            }

            throw new Exception("Failed to delete post!");
        }
    }
}