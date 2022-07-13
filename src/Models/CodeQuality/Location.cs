using System.Text.Json.Serialization;

namespace SemgrepReports.Models.CodeQuality
{
    public class Location
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("lines")]
        public Lines Lines { get; set; }
    }
}
