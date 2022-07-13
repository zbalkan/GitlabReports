using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GitlabReports
{
    public static class SectionExtensions
    {
        public static IContainer IndexedSection(this IContainer container, string sectionName, int level = 1)
        {
            Index.Instance.AddSection(sectionName, level);
            return container.Section(sectionName);
        }
    }
}