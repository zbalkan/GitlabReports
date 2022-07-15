using System.Text.Json.Serialization;

namespace GitlabReports.Models.Sast
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