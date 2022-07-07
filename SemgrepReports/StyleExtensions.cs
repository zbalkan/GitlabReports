using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SemgrepReports
{
    static class StyleExtensions
    {
        private static IContainer Cell(this IContainer container, bool dark)
        {
            return container
                .Border(1)
                .Background(dark ? Colors.Grey.Lighten3 : Colors.White)
                .Padding(5);
        }

        // displays only text label
        public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();

        // allows to inject any type of content, e.g. image
        public static IContainer ValueCell(this IContainer container) => container.Cell(false);

        public static TextSpanDescriptor Title(this TextSpanDescriptor text) => text.Bold().FontSize(32).FontColor(Colors.Grey.Darken4);

        public static TextSpanDescriptor H1(this TextSpanDescriptor text) => text.Bold().FontSize(20).FontColor(Colors.Grey.Darken3);

        public static TextSpanDescriptor H2(this TextSpanDescriptor text) => text.SemiBold().FontSize(16).FontColor(Colors.Grey.Darken2);
        
        public static TextSpanDescriptor HeaderOrFooter(this TextSpanDescriptor text) => text.Light().FontSize(11).FontColor(Colors.Grey.Medium);
    }
}
