using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class CourseStudentModel
    {
        public int CourseId { get; set; }
        public CourseModel Course { get; set; } // navigation property

        public int StudentId { get; set; }
        public StudentModel Student { get; set; } // navigation property
    }
}
