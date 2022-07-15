using GitlabReports.Models.SastReport;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.SastReport
{
    internal sealed class TitlePage : ISection
    {
        public string Title { get; set; }

        private readonly SastReportModel _report;

        public TitlePage(SastReportModel report)
        {
            _report = report;
            Title = $"{_report.ReportType}\n(v{_report.Version})";
        }

        public void Compose(IContainer container) => container
                .Decoration(decoration => decoration
                        .Content()
                        .AlignCenter()
                        .PaddingTop(10, Unit.Centimetre)
                        .Text(Title)
                        .Title());
    }
}