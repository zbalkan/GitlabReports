using System.Text.Json.Serialization;

namespace SemgrepReports.Models.CodeQuality
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
    public class Content
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }


}
