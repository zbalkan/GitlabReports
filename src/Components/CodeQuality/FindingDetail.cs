using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using GitlabReports.Models.CodeQuality;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class FindingDetail : IComponent
    {
        private readonly QualityIssue _issue;

        private readonly int _order;

        public FindingDetail(QualityIssue issue, int order)
        {
            _issue = issue;
            _order = order;
        }

        public void Compose(IContainer container)
        {
            var finding = $"{_order}. {_issue.CheckName} in path: \"{_issue.Location.Path}\" line: {_issue.Location.Lines.Begin}";

            container
                .IndexedSection(finding, 2)
                .Decoration(decoration =>
                {
                    decoration
                         .Before()
                         .PaddingTop(1, Unit.Centimetre)
                         .Text(finding)
                         .H2();

                    decoration
                         .Content()
                         .PaddingTop(0.5f, Unit.Centimetre)
                         .Table(table =>
                         {
                             table.ColumnsDefinition(columns =>
                             {
                                 columns.ConstantColumn(80);
                                 columns.RelativeColumn();
                                 columns.ConstantColumn(80);
                                 columns.RelativeColumn();
                             });

                             // 1st row
                             table.Cell().LabelCell("Name");
                             table.Cell().ColumnSpan(3).ValueCell().Text(_issue.CheckName.Replace('_',' ').ToUpperInvariant());

                             // 2nd row
                             table.Cell().LabelCell("Severity");
                             table.Cell().ValueCell().Text(_issue.Severity.ToUpperInvariant()).FontColorByPriority(_issue.Priority).Bold();

                             table.Cell().LabelCell("Path");
                             table.Cell().ValueCell().Text(_issue.Location.Path);

                             // 3rd row
                             table.Cell().LabelCell("Category");
                             var categories = string.Join(", ", _issue.Categories);
                             table.Cell().ValueCell().Text(categories);

                             table.Cell().LabelCell("Line");
                             table.Cell().ValueCell().Text(_issue.Location.Lines.Begin);

                             // 4th row
                             table.Cell().RowSpan(10).LabelCell("Description");
                             table.Cell().RowSpan(10).ColumnSpan(3).ValueCell().Text(_issue.Description);

                             // 5th row
                             table.Cell().RowSpan(10).LabelCell("Message");
                             table.Cell().RowSpan(10).ColumnSpan(3).ValueCell().Text(_issue.Content?.Body ?? string.Empty);
                         });
                }
            );
        }
    }
}
