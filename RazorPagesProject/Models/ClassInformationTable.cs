namespace RazorPagesProject.Models
{
    public class ClassInformationTable
    {
        public int Id { get; set; } // used in actions, not shown on table
        public string ClassName { get; set; }
        public int StudentCount { get; set; }
        public string Description { get; set; }
    }
}
