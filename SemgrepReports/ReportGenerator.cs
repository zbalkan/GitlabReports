using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SemgrepReports.Components;
using SemgrepReports.Models;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SemgrepReports
{
    internal static class ReportGenerator
    {
        public static Report Import(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new System.ArgumentException($"'{nameof(input)}' cannot be null or empty.", nameof(input));
            }

            var jsonString = File.ReadAllText(input);
            var report = JsonSerializer.Deserialize<Report>(jsonString);
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
                .FontSize(11)
                .FontFamily("Arial");

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

                   column.Item().IndexedSection("Findings").Text("Findings").H1();

                   var vulns = report.Vulnerabilities
                                       .OrderBy(x => x.Priority)
                                       .ThenBy(x => x.Location.File)
                                       .ThenBy(x => x.Location.StartLine)
                                       .ToList();

                   for (int i = 0; i < vulns.Count; i++)
                   {
                       column.Item().Component(new Finding(vulns[i], i + 1));
                   }

                   column.Item().PageBreak();
                   column.Item().Section("Index").PaddingBottom(1, Unit.Centimetre).Text("Index").H1();

                   foreach (var section in Index.Instance.Sections)
                   {
                       column.Item().Row(row =>
                       {
                           row.Spacing(5);
                           for (int i = 1; i < section.Item2; i++)
                           {
                               row.AutoItem().Text("  ");
                           }
                           row.RelativeItem().SectionLink(section.Item1).Text($"- {section.Item1}");
                       });
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
    }
}
