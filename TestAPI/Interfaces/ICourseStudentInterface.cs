using TestAPI.Dto;
using TestAPI.Models;

namespace TestAPI.Interfaces
{
    public interface ICourseStudentInterface
    {
        Task<ICollection<CourseStudentModel>> GetCourseStudents();
        Task<CourseStudentModel> GetCourseStudent(int studentId, int courseId);
        Task<bool> IsCourseStudentExist(int studentId, int courseId);
        Task<bool> DeleteCourseStudent(int studentId, int courseId);
        Task<CourseStudentModel> CreateCourseStudent(CourseStudentDto courseStudentDto);
    }
}
