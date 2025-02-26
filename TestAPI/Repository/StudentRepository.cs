using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Dto;
using TestAPI.Interfaces;
using TestAPI.Models;

namespace TestAPI.Repository
{
    public class StudentRepository : IStudentInterface
    {

        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentModel> AddStudentAsync(StudentDto studentDto)
        {
            if (!await _context.Students.AnyAsync(s => s.StudentId == studentDto.StudentId))
            {
                var newStudent = new StudentModel()
                {
                    StudentId   = studentDto.StudentId,
                    StudentName = studentDto.StudentName,
                    StudentRoll = studentDto.StudentRoll
                };
                await _context.Students.AddAsync(newStudent);
                await _context.SaveChangesAsync();
                return newStudent;
            }
            return new StudentModel();
        }

        public async Task<bool> DeleteStudentByIdAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return false;
            _context.Students.Remove(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<CourseDto>> GetCoursesByStudentIdAsync(int studentId)
        {
            if (!await IsStudentExistAsync(studentId)) return new List<CourseDto>() { };
            var courseIdsAndInsIds = await _context.Students.Where(s => s.StudentId == studentId)
                .Join(_context.CourseStudents,
                s => s.StudentId,
                c => c.StudentId,
                (s, c) => new { s, c })
                .Join(_context.Courses,
                s => s.c.CourseId,
                c => c.CourseId,
                (s,c) => new {c.CourseId, c.InstructorId}).ToListAsync();

            var courseIdsWithInsName = courseIdsAndInsIds.Join(_context.Instructors,
                c => c.InstructorId,
                i => i.InstructorId,
                (c, i) => new { c.CourseId, i.InstructorName }).ToList();

            var courses = new List<CourseDto>();
            foreach(var course in courseIdsWithInsName)
            {
                var courseModel = await _context.Courses.FindAsync(course.CourseId);
                if (courseModel == null) continue;

                courses.Add(new CourseDto()
                {
                    CourseId = courseModel.CourseId,
                    CourseName = courseModel.CourseName,
                    CourseDuration = courseModel.CourseDuration,
                    InstructorName = course.InstructorName
                });
            }
            return courses;
        }

        public async Task<StudentModel> GetStudentByIdAsync(int studentId)
        {
            var student =  await _context.Students.FindAsync(studentId);
            if (student == null) return new StudentModel();
            return student;
        }

        public async Task<ICollection<StudentModel>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<bool> IsStudentExistAsync(int studentId)
        {
            return await _context.Students.AnyAsync(s => s.StudentId == studentId);
        }

        public async Task<StudentModel> UpdateStudentByIdAsync(int studentId, StudentDto studentDto)
        {
            if(await IsStudentExistAsync(studentId))
            {
                var student = await _context.Students.FindAsync(studentDto.StudentId);

                if (student != null)
                {
                    student.StudentName = studentDto.StudentName;
                    student.StudentRoll = studentDto.StudentRoll;
                    _context.Students.Update(student);
                    await _context.SaveChangesAsync();
                    return student;
                }
            }
            return new StudentModel();
        }
    }
}
