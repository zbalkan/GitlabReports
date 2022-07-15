using System.Collections.Generic;
using System.Linq;

namespace GitlabReports.Models.CodeQuality
{
    public class CodeQualityReport : IReport
    {
        private List<QualityIssue> qualityIssues;

        public List<QualityIssue> QualityIssues
        {
            get
            {
                return qualityIssues;
            }
            set
            {
                qualityIssues = value
                    .OrderBy(x => x.Priority)
                                       .ThenBy(x => x.Location.Path)
                                       .ThenBy(x => x.Location.Lines.Begin)
                                       .ToList();
            }
        }

        public string ReportType => "Code Quality Report";
    }
}