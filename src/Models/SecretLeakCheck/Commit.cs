using System.Text.Json.Serialization;

namespace GitlabReports.Models.SecretLeakCheck
{
    public class Commit
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
    }
}