using Microsoft.PowerBI.Api.V2;
using Microsoft.Rest;
using System;
using System.IO;

namespace testPowerBi
{
    class Program
    {
        static void Main(string[] args)
        {
            // try {
                var tokenFileName = "token.txt";
                var tsr = new StreamReader(tokenFileName);
                var tokenString = tsr.ReadToEnd();
            
                var credentials = new TokenCredentials(tokenString);
                var client = new PowerBIClient(credentials);

                var workspaceFileName = "workspace.txt";
                var wsr = new StreamReader(workspaceFileName);
                var workspaceId = wsr.ReadToEnd();
                var reportsResult = client.Reports.GetReportsInGroup(workspaceId);

                foreach (var report in reportsResult.Value)
                {
                    Console.WriteLine("Name:      " + report.Name);
                    Console.WriteLine("Id:        " + report.Id);
                    Console.WriteLine("DatasetId: " + report.DatasetId);
                    Console.WriteLine("EmbedUrl:  " + report.EmbedUrl);
                    Console.WriteLine("WebUrl:    " + report.WebUrl);
                    Console.WriteLine("");
                }
            // } catch {
            //     Console.WriteLine("Something went wrong...");
            // }
        }
    }
}
