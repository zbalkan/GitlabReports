using System;
using System.IO;
using System.Runtime.InteropServices;
using GitlabReports.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GitlabReports
{
    internal static class ReportGenerator
    {
        public static Tuple<IReport, Type> Import(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException($"'{nameof(input)}' cannot be null or empty.", nameof(input));
            }
            var jsonString = File.ReadAllText(input);

            foreach (var item in ModelReportMapping.Map.Values)
            {
                if (item.TryRead(jsonString, out var result))
                {
#pragma warning disable CS8603 // Possible null reference return.
                    return result;
#pragma warning restore CS8603 // Possible null reference return.
                }
            }

            throw new FormatException("The file format is not valid.");
        }

        public static void Export(Tuple<IReport, Type> reportInfo, string output)
        {
            if (reportInfo is null)
            {
                throw new ArgumentNullException(nameof(reportInfo));
            }

            if (string.IsNullOrEmpty(output))
            {
                throw new ArgumentException($"'{nameof(output)}' cannot be null or empty.", nameof(output));
            }

            var report = reportInfo.Item1;
            var type = reportInfo.Item2;

            Document.Create(container => _ = container.Page(page =>
                {
                    SetupPage(page);
                    GenerateHeader(report, page);
                    GenerateContent(report, type, page);
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

        private static void GenerateContent(IReport report, Type type, PageDescriptor page) => ModelReportMapping.Map[type].Generate(report, page);

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