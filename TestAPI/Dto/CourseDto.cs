using TestAPI.Models;

namespace TestAPI.Dto
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public float CourseDuration { get; set; }

        public string InstructorName { get; set; }
    }
}
