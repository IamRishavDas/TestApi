using TestAPI.Dto;
using TestAPI.Interfaces;
using TestAPI.Models;

namespace TestAPI.Repository
{
    public class CourseStudentRepository : ICourseStudentInterface
    {
        public Task<CourseStudentModel> CreateCourseStudent(CourseStudentDto courseStudentDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCourseStudent(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseStudentModel> GetCourseStudent(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CourseStudentModel>> GetCourseStudents()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsCourseStudentExist(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }
    }
}
