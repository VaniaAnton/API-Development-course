using System;
using System.Data;
using System.Globalization;
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

            System.Console.WriteLine(rightNow.ToString());

            Computer computer = new Computer
            {
                MotherBoard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };
            string sql = @"INSERT INTO TutorialAppSchema.Computer(
                MotherBoard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
                ) VALUES('" + computer.MotherBoard
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLTE
                    + "','" + computer.ReleaseDate.ToString("yyyy-MM-dd")
                    + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + computer.VideoCard
                  + "')";

            System.Console.WriteLine(sql);
            int result = dbConnection.Execute(sql);

            System.Console.WriteLine(result);
            // computer.HasWifi = false;
            // Console.WriteLine($"Computer MotherBoard: {computer.MotherBoard}");
            // Console.WriteLine($"Computer CPUCores: {computer.CPUCores}");
            // Console.WriteLine($"Computer HasWifi: {computer.HasWifi}");
            // Console.WriteLine($"Computer HasLTE: {computer.HasLTE}");
            // Console.WriteLine($"Computer ReleaseDate: {computer.ReleaseDate}");
        }
    }
}
