using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class InstructorModel
    {
        [Key]
        public int InstructorId { get; set; }

        [Required]
        [StringLength(100)]
        public string InstructorName { get; set; }

        [Range(0, 100)]
        public int Experience { get; set; }

        public ICollection<CourseModel> Courses { get; set; }
    }
}
