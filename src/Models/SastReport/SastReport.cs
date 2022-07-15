using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabReports.Models.SastReport
{
    public class SastReport : IReport
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("vulnerabilities")]
        public List<Vulnerability> Vulnerabilities { get; set; }

        [JsonPropertyName("scan")]
        public Scan Scan { get; set; }

        public string ReportType => "SAST Report";
    }
}