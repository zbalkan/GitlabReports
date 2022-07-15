using GitlabReports.Models.CodeQuality;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class Findings : ISection
    {
        public string Title { get; set; }

        private readonly CodeQualityReport _report;

        public Findings(CodeQualityReport report)
        {
            _report = report;
            Title = "Finding Details";
        }

        public void Compose(IContainer container) => container
               .Decoration(decoration =>
               {
                   decoration
                       .Before()
                       .PaddingBottom(1, Unit.Centimetre)
                       .Text(Title)
                       .H1();

                   decoration
                   .Content()
                   .Column(column =>
                    {
                        for (var i = 0; i < _report.QualityIssues.Count; i++)
                        {
                            var order = i + 1;
                            var vuln = _report.QualityIssues[i];
                            column.Item().Component(new FindingDetail(vuln, order));
                        }
                    });
               }
           );
    }
}