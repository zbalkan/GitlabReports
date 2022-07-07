using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SemgrepReports.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemgrepReports.Components
{
    internal sealed class ExecutiveSummary : IComponent
    {
        private readonly List<Vulnerability> _vulns;

        public ExecutiveSummary(List<Vulnerability> vulns)
        {
            _vulns = vulns;
        }

        public void Compose(IContainer container)
        {
            var summary = new StringBuilder(100);
            summary.Append("During the scan ").Append(_vulns.Count).Append(" vulnerabilities have been found.")
                .Append(_vulns.Count(v => v.Priority <= 3)).AppendLine(" of them have a higher severity than Medium.")
                .Append("The report includes ")
                .Append(_vulns.Count(v => v.Priority == 1)).Append(" Critical, ")
                .Append(_vulns.Count(v => v.Priority == 2)).Append(" High, ")
                .Append(_vulns.Count(v => v.Priority == 3)).Append(" Medium, and")
                .Append(_vulns.Count(v => v.Priority == 4)).Append(" Low severity vulnerabilities.");

            container
                .Decoration(decoration =>
                {
                    decoration
                        .Before()
                        .Section("Executive Summary")
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
