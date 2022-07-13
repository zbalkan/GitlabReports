using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SemgrepReports.Models.SecretLeakCheck;
using System.Linq;
using System.Text;

namespace SemgrepReports.Components
{
    internal sealed class ExecutiveSummary : IComponent
    {
        private readonly SecretLeakCheckReport _report;

        public ExecutiveSummary(SecretLeakCheckReport report)
        {
            _report = report;
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
