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
            
            //instance of class Dapper
            DataContextDapper dapper = new DataContextDapper(configuration);
            DataContextEF entityFramework = new DataContextEF(configuration);

            //usisg self-created method from DataContextDapper
            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            // System.Console.WriteLine(rightNow.ToString());

            Computer computer = new Computer
            {
                MotherBoard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            entityFramework.Add(computer);
            entityFramework.SaveChanges();
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

            //System.Console.WriteLine(sql);
            //usisg self-created method from DataContextDapper
            bool result = dapper.ExecuteSql(sql);

            //System.Console.WriteLine(result);

            string sqlSelect = @"SELECT 
                Computer.ComputerId,
                Computer.MotherBoard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
               Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            System.Console.WriteLine("'ComputerId','MotherBoard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");

            foreach (Computer singleComputers in computers)
            {
                System.Console.WriteLine("'" + singleComputers.ComputerId
                    + "','" + singleComputers.MotherBoard
                    + "','" + singleComputers.HasWifi
                    + "','" + singleComputers.HasLTE
                    + "','" + singleComputers.ReleaseDate.ToString("yyyy-MM-dd")
                    + "','" + singleComputers.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + singleComputers.VideoCard + "')");
            }

            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
            if (computersEF != null)
            {
                System.Console.WriteLine("'ComputerId','MotherBoard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");
                foreach (Computer singleComputers in computersEF)
                {
                    System.Console.WriteLine("'" + singleComputers.ComputerId
                        + "','" + singleComputers.MotherBoard
                        + "','" + singleComputers.HasWifi
                        + "','" + singleComputers.HasLTE
                        + "','" + singleComputers.ReleaseDate.ToString("yyyy-MM-dd")
                        + "','" + singleComputers.Price.ToString("0.00", CultureInfo.InvariantCulture)
                        + "','" + singleComputers.VideoCard + "')");
                }

            }
            // computer.HasWifi = false;
            // Console.WriteLine($"Computer MotherBoard: {computer.MotherBoard}");
            // Console.WriteLine($"Computer CPUCores: {computer.CPUCores}");
            // Console.WriteLine($"Computer HasWifi: {computer.HasWifi}");
            // Console.WriteLine($"Computer HasLTE: {computer.HasLTE}");
            // Console.WriteLine($"Computer ReleaseDate: {computer.ReleaseDate}");
        }
    }
}
