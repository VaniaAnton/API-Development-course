using System;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using AutoMapper;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

            DataContextDapper dapper = new DataContextDapper(configuration);



            // File.WriteAllText("log.txt", sql);
            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine(sql);

            string computersJson = File.ReadAllText("ComputersSnake.json");

            //this is one way to mapping data
            // Mapper mapper = new Mapper(new MapperConfiguration((cfg) =>
            // {
            //     //simply copy from sourse.computer_id to destination.ComputerId,
            //     //means copy from ComputerSnake to Computer
            //     cfg.CreateMap<ComputerSnake, Computer>()
            //     .ForMember(destination => destination.ComputerId, options =>
            //     options.MapFrom(sourse => sourse.computer_id))
            //     .ForMember(destination => destination.Motherboard, options =>
            //     options.MapFrom(sourse => sourse.motherboard))
            //     .ForMember(destination => destination.CPUCores, options =>
            //     options.MapFrom(sourse => sourse.cpu_cores))
            //     .ForMember(destination => destination.HasWifi, options =>
            //     options.MapFrom(sourse => sourse.has_wifi))
            //     .ForMember(destination => destination.HasLTE, options =>
            //     options.MapFrom(sourse => sourse.has_lte))
            //     .ForMember(destination => destination.ReleaseDate, options =>
            //     options.MapFrom(sourse => sourse.release_date))
            //     .ForMember(destination => destination.Price, options =>
            //     options.MapFrom(sourse => sourse.price))
            //     .ForMember(destination => destination.VideoCard, options =>
            //     options.MapFrom(sourse => sourse.video_card));
            // }));
            //     //  System.Console.WriteLine(computersJson);
            //     // JsonSerializerOptions options = new JsonSerializerOptions()
            //     // {
            //     //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //     // };
            //in my Computer.cs i wrote JsonPropertyName like an another way to solve it
            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            if (computersSystem != null)
            {
                foreach (Computer computer in computersSystem)
                {
                    System.Console.WriteLine(computer.Motherboard);
                }
            }
            //     {
            //         foreach (Computer computer in computersNewtonSoft)
            //         {
            //             // System.Console.WriteLine(computer.Motherboard);
            //             string sql = @"INSERT INTO TutorialAppSchema.Computer(
            //         Motherboard,
            //         HasWifi,
            //         HasLTE,
            //         ReleaseDate,
            //         Price,
            //         VideoCard
            //         ) VALUES('" + EscapeSingleQuote(computer.Motherboard)
            //            + "','" + computer.HasWifi
            //            + "','" + computer.HasLTE
            //            + "','" + computer.ReleaseDate
            //            + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            //            + "','" + EscapeSingleQuote(computer.VideoCard)
            //          + "')";

            //             dapper.ExecuteSql(sql);
            //         }
            //     }

            //     JsonSerializerSettings settings = new JsonSerializerSettings
            //     {
            //         ContractResolver = new CamelCasePropertyNamesContractResolver()
            //     };

            //     string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);
            //     File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            //     string computerCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem);
            //     File.WriteAllText("computersCopySystem.txt", computerCopySystem);
            // }

            // static string EscapeSingleQuote(string input)
            // {
            //     string output = input.Replace("'", "''");

            //     return output;
        }
    }
}