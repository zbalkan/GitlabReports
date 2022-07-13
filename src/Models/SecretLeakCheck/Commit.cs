using System.Text.Json.Serialization;

namespace SemgrepReports.Models.SecretLeakCheck
{
    public class Commit
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
    }
}
