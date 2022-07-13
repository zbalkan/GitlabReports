using System.Text.Json.Serialization;

namespace SemgrepReports.Models.SecretLeakCheck
{
    public class Scanner
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("vendor")]
        public Vendor Vendor { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
