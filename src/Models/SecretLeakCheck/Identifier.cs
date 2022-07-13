using System.Text.Json.Serialization;

namespace SemgrepReports.Models.SecretLeakCheck
{
    public class Identifier
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
