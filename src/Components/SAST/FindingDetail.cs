using GitlabReports.Models.Sast;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.Sast
{
    internal sealed class FindingDetail : IComponent
    {
        private readonly Vulnerability _vuln;

        private readonly int _order;

        public FindingDetail(Vulnerability vuln, int order)
        {
            _vuln = vuln;
            _order = order;
        }

        public void Compose(IContainer container)
        {
            var name = string.IsNullOrEmpty(_vuln.Name) ? _vuln.Message : _vuln.Name;
            var finding = $"{_order}. \"{name}\" in path: \"{_vuln.Location.File}\" line: {_vuln.Location.StartLine}";

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
                             table.Cell().ColumnSpan(3).ValueCell().Text(name);

                             // 2nd row
                             table.Cell().LabelCell("Severity");
                             table.Cell().ValueCell().Text(_vuln.Severity).FontColorByPriority(_vuln.Priority).Bold();

                             table.Cell().LabelCell("File");
                             table.Cell().ValueCell().Text(_vuln.Location.File);

                             // 3rd row
                             table.Cell().LabelCell("Category");
                             table.Cell().ValueCell().Text(_vuln.Category);

                             table.Cell().LabelCell("Line");
                             table.Cell().ValueCell().Text(_vuln.Location.StartLine);

                             // 4th row
                             table.Cell().RowSpan(10).LabelCell("Description");
                             table.Cell().RowSpan(10).ColumnSpan(3).ValueCell().Text(_vuln.Description);

                             // 5th row
                             table.Cell().RowSpan(10).LabelCell("Message");
                             table.Cell().RowSpan(10).ColumnSpan(3).ValueCell().Text(_vuln.Message);
                         });
                }
            );
        }
    }
}