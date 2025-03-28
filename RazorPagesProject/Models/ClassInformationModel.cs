using System.ComponentModel.DataAnnotations;


namespace  RazorPagesProject.Models
{
    public class ClassInformationModel
    {
        private static int _nextId = 1;

        public int Id { get; set; }

        [Required]
        public string ClassName { get; set; }

        [Range(1, 1000, ErrorMessage = "Student count must be between 1 and 1000.")]
        public int StudentCount { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public ClassInformationModel()
        {
            Id = _nextId++;
        }
    }
}