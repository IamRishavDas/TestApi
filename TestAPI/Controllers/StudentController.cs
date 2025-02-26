using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Dto;
using TestAPI.Repository;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentController(StudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync()
        {
            return Ok(_mapper.Map<ICollection<StudentDto>>(await _repository.GetStudentsAsync()));
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentByIdAsync(int studentId)
        {
            if (!await _repository.IsStudentExistAsync(studentId)) return NotFound();
            return Ok(_mapper.Map<StudentDto>(await _repository.GetStudentByIdAsync(studentId)));
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentDto studentDto)
        {
            var newStudent = await _repository.AddStudentAsync(studentDto);
            if (newStudent.StudentName == null) return BadRequest();
            return Ok(_mapper.Map<StudentDto>(newStudent));
        }

        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateStudentAsync(int studentId, [FromBody] StudentDto studentDto)
        {
            var updatedStudent = await _repository.UpdateStudentByIdAsync(studentId, studentDto);
            if (updatedStudent.StudentName == null) return BadRequest();
            return Ok(_mapper.Map<StudentDto>(updatedStudent));
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudentByIdAsync(int studentId)
        {
            if (!await _repository.IsStudentExistAsync(studentId)) return NotFound();
            if(await _repository.DeleteStudentByIdAsync(studentId)) return Ok();
            return BadRequest();
        }

        [HttpGet("{studentId}/courses")]
        public async Task<IActionResult> GetCoursesByStudentIdAsync(int studentId)
        {
            if (!await _repository.IsStudentExistAsync(studentId)) return NotFound();
            var courses = await _repository.GetCoursesByStudentIdAsync(studentId);
            if (courses != null && courses.Count() == 0) return NotFound(new { Message = "No Courses Found!", StatusCode = 400 });
            return Ok(_mapper.Map<List<CourseDto>>(courses));
        }
    }
}
