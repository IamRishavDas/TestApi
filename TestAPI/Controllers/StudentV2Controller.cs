using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Dto;
using TestAPI.Interfaces;

namespace TestAPI.Controllers
{
    [Route("api/v2")]
    [ApiController]
    [ApiVersion("2.0")]
    public class StudentV2Controller : ControllerBase
    {
        private readonly IStudentInterface _repository;
        private readonly IMapper _mapper;
        public StudentV2Controller(IStudentInterface repository, IMapper mapper)
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
    }
}
