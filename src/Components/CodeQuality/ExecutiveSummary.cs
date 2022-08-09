using System.Linq;
using System.Text;
using GitlabReports.Models.CodeQuality;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class ExecutiveSummary : ISection
    {
        public string Title { get; set; }

        private readonly CodeQualityReport _report;

        public ExecutiveSummary(CodeQualityReport report)
        {
            _report = report;
            Title = "Executive Summary";
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
                .IndexedSection(Title)
                .Decoration(decoration =>
                {
                    decoration
                        .Before()
                        .PaddingBottom(1, Unit.Centimetre)
                        .Text(Title)
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