using GitlabReports.Models.Sast;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.Sast
{
    internal sealed class TitlePage : IComponent
    {
        private readonly SastReport _report;

        public TitlePage(SastReport report)
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