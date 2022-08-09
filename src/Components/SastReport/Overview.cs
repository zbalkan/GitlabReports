using GitlabReports.Models.SastReport;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.SastReport
{
    internal sealed class Overview : ISection
    {
        public string Title { get; set; }

        private readonly SastReportModel _report;

        public Overview(SastReportModel report)
        {
            _report = report;
            Title = "Overview";
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
                                 columns.ConstantColumn(80);
                                 columns.RelativeColumn();
                                 columns.ConstantColumn(80);
                                 columns.RelativeColumn();
                             });

                             // Table header
                             table.Cell().ColumnSpan(2).LabelCell("Scan");
                             table.Cell().ColumnSpan(2).LabelCell("Scanner");

                             // 1st row
                             table.Cell().LabelCell("Type");
                             table.Cell().ValueCell().Text(_report.Scan.Type);

                             table.Cell().LabelCell("Name");
                             table.Cell().ValueCell().Text(_report.Scan.Scanner.Name);

                             // 2nd row
                             table.Cell().LabelCell("Start Time");
                             table.Cell().ValueCell().Text(_report.Scan.StartTime);

                             table.Cell().LabelCell("URL");
                             table.Cell().ValueCell().Text(_report.Scan.Scanner.Url);

                             // 3rd row
                             table.Cell().LabelCell("End Time");
                             table.Cell().ValueCell().Text(_report.Scan.EndTime);

                             table.Cell().LabelCell("Vendor");
                             table.Cell().ValueCell().Text(_report.Scan.Scanner.Vendor);

                             // 4th row
                             table.Cell().LabelCell("Status");
                             table.Cell().ValueCell().Text(_report.Scan.Status);

                             table.Cell().LabelCell("Version");
                             table.Cell().ValueCell().Text(_report.Scan.Scanner.Version);
                         });
                });
    }
}