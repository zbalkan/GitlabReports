using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GitlabReports.Components;
using GitlabReports.Models.CodeQuality;
using GitlabReports.Models.SastReport;

namespace GitlabReports
{
    internal static class ModelReportMapping
    {
        private static readonly Dictionary<Type, IReportContent> map = new()
        {
                { typeof(SastReport), new Components.Sast.Content() },
                { typeof(CodeQualityReport), new Components.CodeQuality.Content() }
        };

        public static ReadOnlyDictionary<Type, IReportContent> Map => new(map);
    }
}