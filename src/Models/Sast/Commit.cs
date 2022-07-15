using System.Text.Json.Serialization;

namespace GitlabReports.Models.Sast
{
    public class Commit
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
    }
}