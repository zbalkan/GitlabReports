using System.Text.Json.Serialization;

namespace SemgrepReports.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Commit
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
    }
}
