using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();


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


            File.WriteAllText("log.txt", sql);
            //using StreamWriter openFile = new("log.txt", append: true);

            //openFile.WriteLine(sql);

            System.Console.WriteLine(File.ReadAllText("log.txt"));

        }
    }
}