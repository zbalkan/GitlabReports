using System.Linq;
using QuestPDF.Fluent;
using GitlabReports.Models.CodeQuality;

namespace GitlabReports.Components.CodeQuality
{
    internal static class Content
    {
        public static void Generate(CodeQualityReport report, PageDescriptor page) => page
               .Content()
               .Column(column =>
               {
                   column.Item().Component(new TitlePage(report));
                   column.Item().PageBreak();

                   column.Item().Component(new ExecutiveSummary(report));

                   column.Item().PageBreak();

                   var issues = report.QualityIssues
                                       .OrderBy(x => x.Priority)
                                       .ThenBy(x => x.Location.Path)
                                       .ThenBy(x => x.Location.Lines.Begin)
                                       .ToList();

                   column.Item().Component(new SummaryTable(issues));
                   column.Item().PageBreak();

                   column.Item().IndexedSection("Finding Details").Text("Finding Details").H1();

                   for (var i = 0; i < issues.Count; i++)
                   {
                       var order = i + 1;
                       var issue = issues[i];
                       column.Item().Component(new FindingDetail(issue, order));
                   }
               });
    }
}
