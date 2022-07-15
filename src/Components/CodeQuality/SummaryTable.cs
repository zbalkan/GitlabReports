using System.Collections.Generic;
using GitlabReports.Models.CodeQuality;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class SummaryTable : ISection
    {
        public string Title { get; set; }

        private readonly List<QualityIssue> _issues;

        public SummaryTable(CodeQualityReport report)
        {
            _issues = report.QualityIssues;
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
                                 table.Cell().LabelCell("Quality Issue");

                                 for (var i = 0; i < _issues.Count; i++)
                                 {
                                     var issue = _issues[i];
                                     var order = i + 1;
                                     var finding = $"\"{issue.CheckName.Replace('_', ' ').Capitalize()}\" in path: \"{issue.Location.Path}\" line: {issue.Location.Lines.Begin}";
                                     var link = $"{order}. {finding}";

                                     table.Cell().ValueCell().AlignCenter().Text(order);
                                     table.Cell().ValueCell().Text(issue.Severity.ToUpperInvariant()).FontColorByPriority(issue.Priority).SemiBold();
                                     table.Cell().ValueCell().SectionLink(link).Text(finding);
                                 }
                             });
                });
    }
}