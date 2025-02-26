using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class StudentModel
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentName { get; set; }

        [Range(1, 100000000000)]
        public int StudentRoll { get; set; }
        public ICollection<CourseModel> Courses { get; set; } // navigation property
        public ICollection<CourseStudentModel> CourseStudents { get; set; } // navigation property to the course students
    }
}
