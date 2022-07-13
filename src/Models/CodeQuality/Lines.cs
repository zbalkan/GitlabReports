using System.Text.Json.Serialization;

namespace SemgrepReports.Models.CodeQuality
{
    public class Lines
    {
        [JsonPropertyName("begin")]
        public int Begin { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }
    }


}
