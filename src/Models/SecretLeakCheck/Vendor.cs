using System.Text.Json.Serialization;

namespace GitlabReports.Models.SecretLeakCheck
{
    public class Vendor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
