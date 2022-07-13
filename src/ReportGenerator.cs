using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SemgrepReports.Models;
using SemgrepReports.Models.CodeQuality;
using SemgrepReports.Models.SecretLeakCheck;

namespace SemgrepReports
{
    internal static class ReportGenerator
    {
        public static IReport Import(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException($"'{nameof(input)}' cannot be null or empty.", nameof(input));
            }

            var jsonString = File.ReadAllText(input);
            IReport report;

            try
            {
                report = JsonSerializer.Deserialize<SecretLeakCheckReport>(jsonString, new JsonSerializerOptions());
            }
            catch (JsonException)
            {
                var findings = JsonSerializer.Deserialize<List<QualityIssue>>(jsonString, new JsonSerializerOptions());
                report = new CodeQualityReport() { QualityIssues = findings };
            }
            return report;
        }

        public static void Export(IReport report, string output)
        {
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (string.IsNullOrEmpty(output))
            {
                throw new ArgumentException($"'{nameof(output)}' cannot be null or empty.", nameof(output));
            }

            Document.Create(container => _ = container.Page(page =>
                {
                    SetupPage(page);
                    GenerateHeader(report, page);
                    GenerateContent(report, page);
                    GenerateFooter(page);
                }))
                .GeneratePdf(output);
        }

        private static void SetupPage(PageDescriptor page)
        {
            page.Size(PageSizes.A4);
            page.MarginTop(1.5f, Unit.Centimetre);
            page.MarginRight(2, Unit.Centimetre);
            page.MarginBottom(1, Unit.Centimetre);
            page.MarginLeft(2, Unit.Centimetre);
            page.PageColor(Colors.White);

            var textStyle = new TextStyle()
                .FontSize(10)
                .FontFamily(GetFontByOs());

            page.DefaultTextStyle(textStyle);
        }

        private static void GenerateHeader(IReport report, PageDescriptor page) => page
                .Header()
                .AlignCenter()
                .AlignTop()
                .Text(report.ReportType)
                .HeaderOrFooter();

        private static void GenerateContent(IReport report, PageDescriptor page)
        {
            if (report is SecretLeakCheckReport secretLeakCheckReport)
            {
                Components.SecretLeakCheck.Content.Generate(secretLeakCheckReport, page);
            }
            else if (report is CodeQualityReport codeQualityReport)
            {
                Components.CodeQuality.Content.Generate(codeQualityReport, page);
            }
            else
            {
                throw new FormatException("Invalid JSON file");
            }
        }

        private static void GenerateFooter(PageDescriptor page) => page
                .Footer()
                .Column(column =>
                {
                    column.Item().PaddingVertical(0.2f, Unit.Centimetre).LineHorizontal(1f).LineColor(Colors.Grey.Lighten2);
                    column.Item()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ").HeaderOrFooter();
                            x.CurrentPageNumber().HeaderOrFooter();
                            x.Span(" of ").HeaderOrFooter();
                            x.TotalPages().HeaderOrFooter();
                        });
                });

        private static string GetFontByOs() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Verdana" : "DejaVu Sans";
    }
}
