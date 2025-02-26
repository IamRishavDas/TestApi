using TestAPI.Dto;
using TestAPI.Models;

namespace TestAPI.Interfaces
{
    public interface IStudentInterface
    {
        Task<ICollection<StudentModel>> GetStudentsAsync();
        Task<StudentModel> GetStudentByIdAsync(int studentId);
        Task<StudentModel> AddStudentAsync(StudentDto studentDto);
        Task<bool> DeleteStudentByIdAsync(int studentId);
        Task<StudentModel> UpdateStudentByIdAsync(int studentId, StudentDto studentDto);
        Task<bool> IsStudentExistAsync(int studentId);
        Task<ICollection<CourseDto>> GetCoursesByStudentIdAsync(int studentId);
        
    }
}
