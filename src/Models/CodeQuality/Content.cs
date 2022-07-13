using System.Text.Json.Serialization;

namespace GitlabReports.Models.CodeQuality
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
    public class Content
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }


}
