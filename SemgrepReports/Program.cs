using SemgrepReports.Models;

namespace SemgrepReports
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Source: https://gitlab.com/gitlab-org/security-products/analyzers/secrets/-/raw/master/qa/expect/secrets/gl-secret-detection-report.json
            const string input = "report.json"; // Get path from arguments
            const string output = "report.pdf";
            var report = ReportGenerator.Import(input);
            ReportGenerator.Export(report, output);
        }
    }
}
