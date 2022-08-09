using System;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GitlabReports
{
    public static class StyleExtensions
    {
        private static IContainer Cell(this IContainer container, bool dark) => container
                .Border(1)
                .Background(dark ? Colors.Grey.Lighten3 : Colors.White)
                .Padding(4);

        // displays only text label
        public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();

        // allows to inject any type of content, e.g. image
        public static IContainer ValueCell(this IContainer container) => container.Cell(false);

        public static TextSpanDescriptor Title(this TextSpanDescriptor text) => text.Bold().FontSize(32).FontColor(Colors.Grey.Darken4);

        public static TextSpanDescriptor H1(this TextSpanDescriptor text) => text.Bold().FontSize(18).FontColor(Colors.Grey.Darken3);

        public static TextSpanDescriptor H2(this TextSpanDescriptor text) => text.SemiBold().FontSize(14).FontColor(Colors.Grey.Darken2);

        public static TextSpanDescriptor HeaderOrFooter(this TextSpanDescriptor text) => text.Light().FontSize(9).FontColor(Colors.Grey.Medium);

        public static TextSpanDescriptor FontColorByPriority(this TextSpanDescriptor text, int priority) => text.FontColor(GetSeverityColor(priority));

        public static string SentenceCase(this string text) => string.Concat(text[0].ToString().ToUpperInvariant(), text.ToLowerInvariant().AsSpan(1));

        public static string Capitalize(this string text)
        {
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var sb = new StringBuilder(32);
            foreach (var word in words)
            {
                sb.Append(string.Concat(word[0].ToString().ToUpperInvariant(), word.ToLowerInvariant().AsSpan(1))).Append(' ');
            }

            return sb.ToString();
        }

        private static string GetSeverityColor(int priority) => priority switch
        {
            1 => Colors.Red.Darken4,
            2 => Colors.Red.Medium,
            3 => Colors.Orange.Medium,
            4 => Colors.Yellow.Medium,
            5 => Colors.Green.Medium,
            _ => Colors.Black,
        };
    }
}