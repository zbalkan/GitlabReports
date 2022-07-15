using System;
using System.Linq;
using System.Text.Json;
using GitlabReports.Models;
using GitlabReports.Models.Sast;
using QuestPDF.Fluent;

namespace GitlabReports.Components.Sast
{
    internal sealed class Content : IReportContent
    {
        public void Generate(IReport report, PageDescriptor page) => Generate(report as SastReport, page);

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

        private static void Generate(SastReport report, PageDescriptor page) => page
               .Content()
               .Column(column =>
               {
                   column.Item().Component(new TitlePage(report));
                   column.Item().PageBreak();

                   column.Item().Component(new Overview(report));
                   column.Item().Component(new ExecutiveSummary(report));

                   column.Item().PageBreak();

                   var vulns = report.Vulnerabilities
                                       .OrderBy(x => x.Priority)
                                       .ThenBy(x => x.Location.File)
                                       .ThenBy(x => x.Location.StartLine)
                                       .ToList();

                   column.Item().Component(new SummaryTable(vulns));
                   column.Item().PageBreak();

                   column.Item().IndexedSection("Finding Details").Text("Finding Details").H1();

                   for (var i = 0; i < vulns.Count; i++)
                   {
                       var order = i + 1;
                       var vuln = vulns[i];
                       column.Item().Component(new FindingDetail(vuln, order));
                   }
               });

        private static Tuple<IReport, Type> Serialize(string json)
        {
            var report = JsonSerializer.Deserialize<SastReport>(json, new JsonSerializerOptions());
            return new Tuple<IReport, Type>(report, typeof(SastReport));
        }
    }
}