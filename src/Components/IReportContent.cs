using System;
using GitlabReports.Models;
using QuestPDF.Fluent;

namespace GitlabReports.Components
{
    internal interface IReportContent
    {
        void Generate(IReport report, PageDescriptor page);

        bool TryRead(string json, out Tuple<IReport, Type> result);
    }
}