using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class CourseModel
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Range(1, 10)]
        public float CourseDuration { get; set; }

        public int InstructorId { get; set; }
        public InstructorModel Instructor { get; set; } // navigation property
        public ICollection<StudentModel> Students { get; set; }

        public ICollection<CourseStudentModel> CourseStudents { get; set; }
    }
}
