using System;
using System.Collections.Generic;
using System.Text.Json;
using GitlabReports.Models;
using GitlabReports.Models.CodeQuality;
using QuestPDF.Fluent;

namespace GitlabReports.Components.CodeQuality
{
    internal sealed class Content : IReportContent
    {
        public ISection TitlePage { get; set; }

        public ISection? Overview { get; set; }

        public ISection ExecutiveSummary { get; set; }

        public ISection SummaryTable { get; set; }

        public ISection Findings { get; set; }

        public void Generate(IReport report, PageDescriptor page) => Generate((CodeQualityReport)report, page);

        public bool TryRead(string json, out Tuple<IReport, Type>? result)
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

        private static Tuple<IReport, Type>? Serialize(string json)
        {
            var findings = JsonSerializer.Deserialize<List<QualityIssue>>(json, new JsonSerializerOptions());
            if (findings == null)
            {
                return null;
            }

            var report = new CodeQualityReport() { QualityIssues = findings };
            return new Tuple<IReport, Type>(report, typeof(CodeQualityReport));
        }

        private void Generate(CodeQualityReport report, PageDescriptor page)
        {
            TitlePage = new TitlePage(report);
            ExecutiveSummary = new ExecutiveSummary(report);
            SummaryTable = new SummaryTable(report);
            Findings = new Findings(report);

            page
               .Content()
               .Column(column =>
               {
                   column.Item().Component(TitlePage);
                   column.Item().PageBreak();
                   column.Item().Component(ExecutiveSummary);
                   column.Item().PageBreak();
                   column.Item().Component(SummaryTable);
                   column.Item().PageBreak();
                   column.Item().Component(Findings);
               });
        }
    }
}