using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GitlabReports.Models;
using GitlabReports.Models.CodeQuality;
using QuestPDF.Fluent;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class Content : IReportContent
    {
        public void Generate(IReport report, PageDescriptor page) => Generate(report as CodeQualityReport, page);

        public bool TryRead(string json, out Tuple<IReport, Type> result)
        {
            try
            {
                result = Serialize(json);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        private static Tuple<IReport, Type> Serialize(string json)
        {
            var findings = JsonSerializer.Deserialize<List<QualityIssue>>(json, new JsonSerializerOptions());
            var report = new CodeQualityReport() { QualityIssues = findings };
            return new Tuple<IReport, Type>(report, typeof(CodeQualityReport));
        }

        private static void Generate(CodeQualityReport report, PageDescriptor page) => page
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