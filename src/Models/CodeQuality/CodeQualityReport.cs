using System.Collections.Generic;

namespace GitlabReports.Models.CodeQuality
{
    public class CodeQualityReport : IReport
    {
        public List<QualityIssue> QualityIssues { get; set; }

        public string ReportType => "Code Quality Report";
    }
}
