using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemgrepReports.Models.SecretLeakCheck
{
    public class Report
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("vulnerabilities")]
        public List<Vulnerability> Vulnerabilities { get; set; }

        [JsonPropertyName("scan")]
        public Scan Scan { get; set; }
    }
}