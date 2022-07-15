using GitlabReports.Models.SastReport;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.SastReport
{
    internal sealed class TitlePage : IComponent
    {
        private readonly SastReportModel _report;

        public TitlePage(SastReportModel report)
        {
            _report = report;
        }

        public void Compose(IContainer container) => container
                .Decoration(decoration => decoration
                        .Content()
                        .AlignCenter()
                        .PaddingTop(10, Unit.Centimetre)
                        .Text($"{_report.ReportType}\n(v{_report.Version})")
                        .Title());
    }
}