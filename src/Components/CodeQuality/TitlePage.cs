using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SemgrepReports.Models.CodeQuality;

namespace SemgrepReports.Components.CodeQuality
{
    internal sealed class TitlePage : IComponent
    {
        private readonly CodeQualityReport _report;

        public TitlePage(CodeQualityReport report)
        {
            _report = report;
        }

        public void Compose(IContainer container) => container
                .Decoration(decoration => decoration
                        .Content()
                        .AlignCenter()
                        .PaddingTop(10, Unit.Centimetre)
                        .Text(_report.ReportType)
                        .Title());
    }
}
