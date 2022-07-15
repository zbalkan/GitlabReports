using System.Text.Json.Serialization;

namespace GitlabReports.Models.Sast
{
    public class Vendor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}