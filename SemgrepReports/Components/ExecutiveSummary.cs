using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SemgrepReports.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemgrepReports.Components
{
    internal class ExecutiveSummary : IComponent
    {
        private readonly List<Vulnerability> _vulns;

        public ExecutiveSummary(List<Vulnerability> vulns)
        {
            _vulns = vulns;
        }

        public void Compose(IContainer container)
        {
            var summary = new StringBuilder();
            summary.Append("During the scan ").Append(_vulns.Count).Append(" vulnerabilities have been found.")
                .Append(_vulns.Where(v => v.Priority <= 3).Count()).AppendLine(" of them have a higher severity than Medium.")
                .Append("The report includes ")
                .Append(_vulns.Where(v => v.Priority == 1).Count()).Append(" Critical, ")
                .Append(_vulns.Where(v => v.Priority == 2).Count()).Append(" High, ")
                .Append(_vulns.Where(v => v.Priority == 3).Count()).Append(" Medium, and")
                .Append(_vulns.Where(v => v.Priority == 4).Count()).Append(" Low severity vulnerabilities.");


            container
                .Decoration(decoration =>
                {
                    decoration
                        .Before()
                        .Text("Executive Summary")
                        .H2();

                    decoration
                    .Content()
                    .Text(summary.ToString());
                }
            );
        }
    }
}
