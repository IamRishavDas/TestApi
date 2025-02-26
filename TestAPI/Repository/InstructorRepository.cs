using TestAPI.Dto;
using TestAPI.Interfaces;
using TestAPI.Models;

namespace TestAPI.Repository
{
    public class InstructorRepository : IInstructorInterface
    {
        public Task<InstructorModel> CreateInstructor(InstructorDto instructorDto)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorModel> GetInstructorById(int instructorId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<InstructorModel>> GetInstructors()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInstructorExist(int instructorId)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorModel> UpdateInstructor(int instructorId, InstructorDto instructorDto)
        {
            throw new NotImplementedException();
        }
    }
}
