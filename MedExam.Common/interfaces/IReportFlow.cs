namespace MedExam.Common.Interfaces
{
    public interface IReportFlow
    {
        string Name { get; set; }
        string Title { get; set; }
        string Template { get; set; }
    }
}