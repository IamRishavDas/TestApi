using TestAPI.Dto;
using TestAPI.Models;

namespace TestAPI.Interfaces
{
    public interface IInstructorInterface
    {
        Task<ICollection<InstructorModel>> GetInstructors();
        Task<InstructorModel> GetInstructorById(int instructorId);
        Task<bool> IsInstructorExist(int instructorId);
        Task<InstructorModel> UpdateInstructor(int instructorId, InstructorDto instructorDto);
        Task<InstructorModel> CreateInstructor(InstructorDto instructorDto);
    }
}
