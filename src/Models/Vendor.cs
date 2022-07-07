using System.Text.Json.Serialization;

namespace SemgrepReports.Models
{
    public class Vendor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
