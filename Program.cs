using Microsoft.PowerBI.Api.V2;
using Microsoft.Rest;
using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace testPowerBi
{
    class Program
    {
        static void Main(string[] args)
        {
            var configFilename = "config.yaml";
            var input = new StreamReader(configFilename);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            var config = deserializer.Deserialize<Cfg>(input);
            var credentials = new TokenCredentials(config.token);
            var client = new PowerBIClient(credentials);
            var reportsResult = client.Reports.GetReportsInGroup(config.workspaceid);

            foreach (var report in reportsResult.Value)
            {
                Console.WriteLine("Name:      " + report.Name);
                Console.WriteLine("Id:        " + report.Id);
                Console.WriteLine("DatasetId: " + report.DatasetId);
                Console.WriteLine("EmbedUrl:  " + report.EmbedUrl);
                Console.WriteLine("WebUrl:    " + report.WebUrl);
                Console.WriteLine("");
            }
        }

        public class Cfg
        {
            public string token { get; set; }
            public string workspaceid {get; set; }
        }
    }
}
