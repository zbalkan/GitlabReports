using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using GitlabReports.Models;
using GitlabReports.Models.SecretLeakCheck;
using QuestPDF.Fluent;

namespace GitlabReports.Components.SecretLeakCheck
{
    internal sealed class Content : IReportContent
    {
        public void Generate(IReport report, PageDescriptor page) => Generate(report as SecretLeakCheckReport, page);

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

        private static void Generate(SecretLeakCheckReport report, PageDescriptor page) => page
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
            var report = JsonSerializer.Deserialize<SecretLeakCheckReport>(json, new JsonSerializerOptions());
            return new Tuple<IReport, Type>(report, typeof(SecretLeakCheckReport));
        }
    }
}