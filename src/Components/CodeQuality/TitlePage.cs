using GitlabReports.Models.CodeQuality;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class TitlePage : ISection
    {
        public string Title { get; set; }

        private readonly CodeQualityReport _report;

        public TitlePage(CodeQualityReport report)
        {
            _report = report;
            Title = _report.ReportType;
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