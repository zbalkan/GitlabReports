using System.Text.Json.Serialization;

namespace GitlabReports.Models.SastReport
{
    public class Commit
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
    }
}