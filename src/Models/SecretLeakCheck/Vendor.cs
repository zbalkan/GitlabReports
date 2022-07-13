using System.Text.Json.Serialization;

namespace SemgrepReports.Models.SecretLeakCheck
{
    public class Vendor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
