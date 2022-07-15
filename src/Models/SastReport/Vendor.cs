using System.Text.Json.Serialization;

namespace GitlabReports.Models.SastReport
{
    public class Vendor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}