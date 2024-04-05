using System;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1";

            IDbConnection dbConnection = new SqlConnection(connectionString);

            string sqlCommand = "SELECT GETDATE()";

            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);

            System.Console.WriteLine(rightNow);
            // Computer computer = new Computer
            // {
            //     MotherBoard = "ASUS",
            //     CPUCores = 4,
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = new DateTime(2020, 1, 1),
            //     Price = 1000,
            //     VideoCard = "Nvidia"
            // };
            // computer.HasWifi = false;
            // Console.WriteLine($"Computer MotherBoard: {computer.MotherBoard}");
            // Console.WriteLine($"Computer CPUCores: {computer.CPUCores}");
            // Console.WriteLine($"Computer HasWifi: {computer.HasWifi}");
            // Console.WriteLine($"Computer HasLTE: {computer.HasLTE}");
            // Console.WriteLine($"Computer ReleaseDate: {computer.ReleaseDate}");
        }
    }
}
