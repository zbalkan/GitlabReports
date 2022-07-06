using SemgrepReports.Models;
using System;
using System.IO;
using System.Text.Json;

namespace SemgrepReports
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Source: https://gitlab.com/gitlab-org/security-products/analyzers/secrets/-/raw/master/qa/expect/secrets/gl-secret-detection-report.json
            const string fileName = "report.json"; // Get path from arguments
            string jsonString = File.ReadAllText(fileName);
            var report = JsonSerializer.Deserialize<Root>(jsonString);
            Console.WriteLine(report.Scan.Status);
        }
    }
}
