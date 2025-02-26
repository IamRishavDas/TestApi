using AutoMapper;
using TestAPI.Dto;
using TestAPI.Models;

namespace TestAPI.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<InstructorModel, InstructorDto>();
            CreateMap<StudentModel, StudentDto>();
            CreateMap<CourseStudentModel, CourseStudentDto>();
            CreateMap<CourseModel, CourseDto>()
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.InstructorName));
        }
    }
}
