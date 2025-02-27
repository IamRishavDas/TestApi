using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Dto;
using TestAPI.Interfaces;

namespace TestAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StudentV1Controller : ControllerBase
    {
        private readonly IStudentInterface _repository;
        private readonly IMapper _mapper;

        public StudentV1Controller(IStudentInterface repository, IMapper mapper)
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

        [HttpGet("FindByIdAndName")]
        public async Task<ActionResult<StudentDto>> GetStudentByIdAndName([FromQuery] int id=1, [FromQuery] string name = "value")
        {
            var studentDto = await _repository.GetStudentByIdAndName(id, name);
            if (studentDto.StudentName == null) return NotFound($"Student Not Found id: {id}, name: {name}");
            return Ok(studentDto);
        }
    }
}
