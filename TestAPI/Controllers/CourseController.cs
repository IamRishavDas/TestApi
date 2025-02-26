using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Dto;
using TestAPI.Repository;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseRepository _repository;
        private readonly IMapper _mapper;
        public CourseController(CourseRepository courseRepository, IMapper mapper)
        {
            _repository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(_mapper.Map<List<CourseDto>>(await _repository.GetCourses()));
        }


        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            if (!await _repository.IsCourseExistAsync(courseId)) return NotFound();
            var course = await _repository.GetCourseById(courseId);
            if (course == null) return NotFound();
            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto courseDto)
        {
            if (await _repository.IsCourseExistAsync(courseDto.CourseId)) return BadRequest();
            return Ok(_mapper.Map<CourseDto>(await _repository.CreateCourse(courseDto)));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourseById(int courseId)
        {
            if (!await _repository.IsCourseExistAsync(courseId)) return NotFound();
            return Ok(_mapper.Map<List<CourseDto>>(await _repository.DeleteCourse(courseId)));
        }
    }
}
