using QuestPDF.Infrastructure;

namespace GitlabReports.Components
{
    internal interface ISection : IComponent
    {
        public string Title { get; set; }
    }
}
