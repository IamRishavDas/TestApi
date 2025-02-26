using TestAPI.Dto;
using TestAPI.Models;

namespace TestAPI.Interfaces
{
    public interface ICoursesInterface
    {
        Task<CourseModel> GetCourseById(int courseId);
        Task<ICollection<CourseModel>> GetCourses();
        Task<bool> IsCourseExistAsync(int courseId);
        Task<CourseModel> CreateCourse(CourseDto courseDto);
        Task<bool> DeleteCourse(int courseId);
    }
}
