using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SemgrepReports.Components;
using SemgrepReports.Models;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace SemgrepReports
{
    internal static class ReportGenerator
    {
        public static Report Import(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException($"'{nameof(input)}' cannot be null or empty.", nameof(input));
            }

            var jsonString = File.ReadAllText(input);
            var defaultOptions = new JsonSerializerOptions();
            var report = JsonSerializer.Deserialize<Report>(jsonString, defaultOptions);
            return report;
        }

        public static void Export(Report report, string output)
        {
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (string.IsNullOrEmpty(output))
            {
                throw new ArgumentException($"'{nameof(output)}' cannot be null or empty.", nameof(output));
            }

            Document.Create(container =>
            {
                _ = container.Page(page =>
                {
                    SetupPage(page);
                    GenerateHeader(report, page);
                    GenerateContent(report, page);
                    GenerateFooter(page);
                });
            })
                .GeneratePdf(output);
        }

        private static void SetupPage(PageDescriptor page)
        {
            page.Size(PageSizes.A4);
            page.MarginTop(2, Unit.Centimetre);
            page.MarginRight(2, Unit.Centimetre);
            page.MarginBottom(1, Unit.Centimetre);
            page.MarginLeft(2, Unit.Centimetre);
            page.PageColor(Colors.White);

            var textStyle = new TextStyle()
                .FontSize(10)
                .FontFamily(GetFontByOs());

            page.DefaultTextStyle(textStyle);
        }

        private static void GenerateHeader(Report report, PageDescriptor page)
        {
            page
                .Header()
                .AlignCenter()
                .Text($"Static Application Security Testing (SAST) Report (v{report.Version})")
                .HeaderOrFooter();
        }

        private static void GenerateContent(Report report, PageDescriptor page)
        {
            page
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
                       column.Item().Component(new Finding(vuln, order));
                   }
               });
        }

        private static void GenerateFooter(PageDescriptor page)
        {
            page
                .Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Page ").HeaderOrFooter();
                    x.CurrentPageNumber().HeaderOrFooter();
                    x.Span(" of ").HeaderOrFooter();
                    x.TotalPages().HeaderOrFooter();
                });
        }
        private static string GetFontByOs() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Verdana" : "DejaVu Sans";
    }
}
