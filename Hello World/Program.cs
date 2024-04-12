using System;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
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
            //using StreamWriter openFile = new("log.txt", append: true);

            //openFile.WriteLine(sql);

            string computersJson = File.ReadAllText("Computers.json");

            //  System.Console.WriteLine(computersJson);
            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computersNewtonSoft != null)
            {
                foreach (Computer computer in computersNewtonSoft)
                {
                    // System.Console.WriteLine(computer.Motherboard);
                    string sql = @"INSERT INTO TutorialAppSchema.Computer(
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
                ) VALUES('" + EscapeSingleQuote(computer.Motherboard)
                   + "','" + computer.HasWifi
                   + "','" + computer.HasLTE
                   + "','" + computer.ReleaseDate
                   + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                   + "','" + EscapeSingleQuote(computer.VideoCard)
                 + "')";

                    dapper.ExecuteSql(sql);
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);
            File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            string computerCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem);
            File.WriteAllText("computersCopySystem.txt", computerCopySystem);
        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }
    }
}