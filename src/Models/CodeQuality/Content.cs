using System.Text.Json.Serialization;

namespace GitlabReports.Models.CodeQuality
{
    public class Content
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}