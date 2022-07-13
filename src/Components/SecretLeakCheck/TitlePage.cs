using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using GitlabReports.Models.SecretLeakCheck;

namespace GitlabReports.Components.SecretLeakCheck
{
    internal sealed class TitlePage : IComponent
    {
        private readonly SecretLeakCheckReport _report;

        public TitlePage(SecretLeakCheckReport report)
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
