using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DotnetAPI.Data
{
    class DataContextDapper
    {
        private string? _connectionString;
        public DataContextDapper(IConfiguration configuration)
        {
            // _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            //get data from JSON by GetConnectionString()
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }
        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }
        public IEnumerable<T> LoadDataWithParameters<T>(string sql, DynamicParameters parameters)
        {
            //get data from JSON by GetConnectionString()
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql, parameters);
        }
        public T LoadDataSingleWithParameters<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql, parameters);
        }
        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (dbConnection.Execute(sql) > 0);
        }
        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }

        public bool ExecuteSqlWithParameters(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (dbConnection.Execute(sql, parameters) > 0);

            // SqlCommand commandWithParams = new SqlCommand(sql);

            // foreach (SqlParameter parameter in parameters)
            // {
            //     commandWithParams.Parameters.Add(parameter);
            // }

            // SqlConnection dbConnection = new SqlConnection(_connectionString);
            // dbConnection.Open();

            // commandWithParams.Connection = dbConnection;

            // int rowsAffected = commandWithParams.ExecuteNonQuery();

            // dbConnection.Close();

            // return rowsAffected > 0;
        }

    }
}