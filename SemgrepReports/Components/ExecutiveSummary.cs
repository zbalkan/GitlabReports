using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SemgrepReports.Models;
using System.Linq;
using System.Text;

namespace SemgrepReports.Components
{
    internal sealed class ExecutiveSummary : IComponent
    {
        private readonly Report _report;

        public ExecutiveSummary(Report report)
        {
            _report = report;
        }

        public void Compose(IContainer container)
        {
            var summary = new StringBuilder(100);
            summary.Append("During the scan ").Append(_report.Vulnerabilities.Count).Append(" vulnerabilities have been found.")
                .Append(_report.Vulnerabilities.Count(v => v.Priority <= 3)).AppendLine(" of them have a higher severity than Medium. ")
                .Append("The report includes ")
                .Append(_report.Vulnerabilities.Count(v => v.Priority == 1)).Append(" Critical, ")
                .Append(_report.Vulnerabilities.Count(v => v.Priority == 2)).Append(" High, ")
                .Append(_report.Vulnerabilities.Count(v => v.Priority == 3)).Append(" Medium, and ")
                .Append(_report.Vulnerabilities.Count(v => v.Priority == 4)).Append(" Low severity vulnerabilities.");

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
                    .Text(summary.ToString());
                }
            );
        }
    }
}
