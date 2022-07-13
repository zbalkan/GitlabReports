using System.Linq;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using GitlabReports.Models.CodeQuality;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class ExecutiveSummary : IComponent
    {
        private readonly CodeQualityReport _report;

        public ExecutiveSummary(CodeQualityReport report)
        {
            _report = report;
        }

        public void Compose(IContainer container)
        {
            var summary = new StringBuilder(100);
            summary.Append("During the scan ").Append(_report.QualityIssues.Count).Append(" code quality issues have been found. ")
                .Append(_report.QualityIssues.Count(v => v.Priority <= 3)).AppendLine(" of them have a higher severity than Major. ")
.AppendLine("The report includes:")
                .Append("  - ").Append(_report.QualityIssues.Count(v => v.Priority == 1)).AppendLine(" Blocker, ")
                .Append("  - ").Append(_report.QualityIssues.Count(v => v.Priority == 2)).AppendLine(" Critical, ")
                .Append("  - ").Append(_report.QualityIssues.Count(v => v.Priority == 3)).AppendLine(" Major, ")
                .Append("  - ").Append(_report.QualityIssues.Count(v => v.Priority == 4)).AppendLine(" Minor, ")
                .Append("  - ").Append(_report.QualityIssues.Count(v => v.Priority == 5)).AppendLine(" Info severity issues.");

            container
                .IndexedSection("Executive Summary")
                .Decoration(decoration =>
                {
                    decoration
                        .Before()
                        .PaddingBottom(1, Unit.Centimetre)
                        .Text("Executive Summary")
                        .H1();

                    decoration
                    .Content()
                    .Text(summary.ToString())
                    .LineHeight(1.5f);
                }
            );
        }
    }
}