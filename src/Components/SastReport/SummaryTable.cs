﻿using System.Collections.Generic;
using GitlabReports.Models.SastReport;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.SastReport
{
    internal sealed class SummaryTable : ISection
    {
        public string Title { get; set; }

        private readonly List<Vulnerability> _vulns;

        public SummaryTable(SastReportModel report)
        {
            _vulns = report.Vulnerabilities;
            Title = "Finding Summary";
        }

        public void Compose(IContainer container) => container
                .IndexedSection(Title)
                .Decoration(decoration =>
                {
                    decoration
                    .Before()
                    .Text(Title)
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

                                 for (var i = 0; i < _vulns.Count; i++)
                                 {
                                     var vuln = _vulns[i];
                                     var order = i + 1;
                                     var name = string.IsNullOrEmpty(vuln.Name) ? vuln.Message : vuln.Name;
                                     var finding = $"\"{name}\" in path: \"{vuln.Location.File}\" line: {vuln.Location.StartLine}";
                                     var link = $"{order}. {finding}";

                                     table.Cell().ValueCell().AlignCenter().Text(order);
                                     table.Cell().ValueCell().Text(vuln.Severity).FontColorByPriority(vuln.Priority).SemiBold();
                                     table.Cell().ValueCell().SectionLink(link).Text(finding);
                                 }
                             });
                });
    }
}