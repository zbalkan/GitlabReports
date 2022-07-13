using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemgrepReports.Models.CodeQuality
{
    public class CodeQualityReport : IReport
    {
        [JsonPropertyName("engine_name")]
        public string EngineName { get; set; }

        [JsonPropertyName("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }

        [JsonPropertyName("check_name")]
        public string CheckName { get; set; }

        [JsonPropertyName("content")]
        public Content Content { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("other_locations")]
        public List<object> OtherLocations { get; set; }

        [JsonPropertyName("remediation_points")]
        public int RemediationPoints { get; set; }

        [JsonPropertyName("severity")]
        public string Severity { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
        public string ReportType => "Code Quality Report";
    }
}
