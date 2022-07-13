using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabReports.Models.CodeQuality
{
    public class QualityIssue
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

        public int Priority => Severity switch
        {
            "blocker" => 1,
            "critical" => 2,
            "major" => 3,
            "minor" => 4,
            "info" => 5,
            _ => 0,
        };
    }
}