using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SemgrepReports.Models;
using System.Collections.Generic;

namespace SemgrepReports.Components
{
    internal sealed class SummaryTable : IComponent
    {
        private readonly List<Vulnerability> _vulns;

        public SummaryTable(List<Vulnerability> vulns)
        {
            _vulns = vulns;
        }

        public void Compose(IContainer container)
        {
            container
                .IndexedSection("Finding Summary")
                .Decoration(decoration =>
                {
                    decoration
                    .Before()
                    .Text("Finding Summary")
                    .H1();

                    decoration
                             .Content()
                             .PaddingTop(1, Unit.Centimetre)
                             .PaddingBottom(1, Unit.Centimetre)
                             .Table(table =>
                             {
                                 table.ColumnsDefinition(columns =>
                                 {
                                     columns.ConstantColumn(60);
                                     columns.ConstantColumn(60);
                                     columns.RelativeColumn();
                                 });

                                // Table header
                                 table.Cell().LabelCell("Number");
                                 table.Cell().LabelCell("Severity");
                                 table.Cell().LabelCell("Vulnerability");

                                 for (int i = 0; i < _vulns.Count; i++)
                                 {
                                     var vuln = _vulns[i];
                                     table.Cell().ValueCell().AlignCenter().Text(i + 1);
                                     table.Cell().ValueCell().Text(vuln.Severity).FontColorByPriority(vuln.Priority).SemiBold();
                                     table.Cell().ValueCell().Text(vuln.Name);
                                 }
                             });
                });
        }
    }
}
