using System;
using System.Text.Json;
using GitlabReports.Models;
using GitlabReports.Models.SastReport;
using QuestPDF.Fluent;

namespace GitlabReports.Components.SastReport
{
    internal sealed class Content : IReportContent
    {
        public ISection TitlePage { get; set; }

        public ISection? Overview { get; set; }

        public ISection ExecutiveSummary { get; set; }

        public ISection SummaryTable { get; set; }

        public ISection Findings { get; set; }

        public void Generate(IReport report, PageDescriptor page) => Generate((SastReportModel)report, page);

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

        private void Generate(SastReportModel report, PageDescriptor page)
        {
            TitlePage = new TitlePage(report);
            Overview = new Overview(report);
            ExecutiveSummary = new ExecutiveSummary(report);
            SummaryTable = new SummaryTable(report);
            Findings = new Findings(report);

            page
               .Content()
               .Column(column =>
               {
                   column.Item().Component(TitlePage);
                   column.Item().PageBreak();
                   column.Item().Component(Overview);
                   column.Item().Component(ExecutiveSummary);
                   column.Item().PageBreak();
                   column.Item().Component(SummaryTable);
                   column.Item().PageBreak();
                   column.Item().Component(Findings);
               });
        }

        private static Tuple<IReport, Type>? Serialize(string json)
        {
            var report = JsonSerializer.Deserialize<SastReportModel>(json, new JsonSerializerOptions());

            return report == null
                ? null
                : new Tuple<IReport, Type>(report, typeof(SastReportModel));
        }
    }
}