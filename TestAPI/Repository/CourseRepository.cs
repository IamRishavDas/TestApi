using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Dto;
using TestAPI.Interfaces;
using TestAPI.Models;

namespace TestAPI.Repository
{
    public class CourseRepository : ICoursesInterface
    {

        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CourseModel> CreateCourse(CourseDto courseDto)
        {
            if (await IsCourseExistAsync(courseDto.CourseId)) return new CourseModel() { };
            var newCourse = new CourseModel()
            {
                CourseId = courseDto.CourseId,
                CourseName = courseDto.CourseName,
                CourseDuration = courseDto.CourseDuration,
            };
            await _context.Courses.AddAsync(newCourse);
            if (await _context.SaveChangesAsync() > 0) return newCourse;
            return new CourseModel() { };
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            if (!await IsCourseExistAsync(courseId)) return false;
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return false;
            _context.Courses.Remove(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CourseModel> GetCourseById(int courseId)
        {
            if (!await IsCourseExistAsync(courseId)) return new CourseModel() { };
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return new CourseModel() { };
            return course;
        }

        public async Task<ICollection<CourseModel>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<bool> IsCourseExistAsync(int courseId)
        {
            return await _context.Courses.AnyAsync(c => c.CourseId == courseId);
        }
    }
}
