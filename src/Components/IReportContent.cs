using System;
using GitlabReports.Models;
using QuestPDF.Fluent;

namespace GitlabReports.Components
{
    internal interface IReportContent
    {
        ISection TitlePage { get; set; }

        ISection? Overview { get; set; }

        ISection ExecutiveSummary { get; set; }

        ISection SummaryTable { get; set; }

        ISection Findings { get; set; }

        void Generate(IReport report, PageDescriptor page);

        bool TryRead(string json, out Tuple<IReport, Type>? result);
    }
}