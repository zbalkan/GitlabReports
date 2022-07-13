using System;
using System.Text.Json.Serialization;

namespace GitlabReports.Models.SecretLeakCheck
{
    public class Scan
    {
        [JsonPropertyName("scanner")]
        public Scanner Scanner { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}