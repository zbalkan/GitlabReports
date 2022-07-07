using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SemgrepReports.Models;

namespace SemgrepReports.Components
{
    internal sealed class Finding : IComponent
    {
        private readonly Vulnerability _vuln;

        public Finding(Vulnerability vuln)
        {
            _vuln = vuln;
        }

        public void Compose(IContainer container)
        {
            var finding = $"{_vuln.Name} in {_vuln.Location.File}:L{_vuln.Location.StartLine}";

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
                             table.Cell().ColumnSpan(3).ValueCell().Text(_vuln.Name);

                             // 2nd row
                             table.Cell().LabelCell("Severity");
                             table.Cell().ValueCell().Text(_vuln.Severity).FontColor(GetSeverityColor(_vuln.Priority)).Bold();

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
                             // 4th row
                             table.Cell().RowSpan(10).LabelCell("Message");
                             table.Cell().RowSpan(10).ColumnSpan(3).ValueCell().Text(_vuln.Message);
                         });
                }
            );
        }

        private static string GetSeverityColor(int priority)
        {
            return priority switch
            {
                1 => Colors.Red.Medium,
                2 => Colors.Orange.Medium,
                3 => Colors.Yellow.Medium,
                4 => Colors.Green.Medium,
                _ => Colors.Black,
            };
        }
    }
}
