using System.Linq;
using System.Text;
using GitlabReports.Models.SastReport;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.SastReport
{
    internal sealed class ExecutiveSummary : ISection
    {
        public string Title { get; set; }

        private readonly SastReportModel _report;

        public ExecutiveSummary(SastReportModel report)
        {
            _report = report;
            Title = "Executive Summary";
        }

        public void Compose(IContainer container)
        {
            var summary = new StringBuilder(100);
            summary.Append("During the scan ").Append(_report.Vulnerabilities.Count).Append(" vulnerabilities have been found. ")
                .Append(_report.Vulnerabilities.Count(v => v.Priority <= 3)).AppendLine(" of them have a higher severity than Medium. ")
                .AppendLine("The report includes:")
                .Append("  - ").Append(_report.Vulnerabilities.Count(v => v.Priority == 1)).AppendLine(" Critical, ")
                .Append("  - ").Append(_report.Vulnerabilities.Count(v => v.Priority == 2)).AppendLine(" High, ")
                .Append("  - ").Append(_report.Vulnerabilities.Count(v => v.Priority == 3)).AppendLine(" Medium, ")
                .Append("  - ").Append(_report.Vulnerabilities.Count(v => v.Priority == 4)).AppendLine(" Low severity vulnerabilities.");

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