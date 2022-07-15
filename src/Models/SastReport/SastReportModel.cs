using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GitlabReports.Models.SastReport
{
    public class SastReportModel : IReport
    {
        private List<Vulnerability> vulnerabilities;

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("vulnerabilities")]
        public List<Vulnerability> Vulnerabilities
        {
            get
            {
                return vulnerabilities;
            }
            set
            {
                vulnerabilities = value
                    .OrderBy(x => x.Priority)
                    .ThenBy(x => x.Location.File)
                    .ThenBy(x => x.Location.StartLine)
                    .ToList();
            }
        }

        [JsonPropertyName("scan")]
        public Scan Scan { get; set; }

        public string ReportType => "SAST Report";
    }
}