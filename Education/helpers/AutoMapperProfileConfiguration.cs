using AutoMapper;
using Education.Models;

namespace Education.helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {

        public AutoMapperProfileConfiguration()
        {


            CreateMap<Student, NewStudentData>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            
            .ForMember(dest => dest.StudentRequests, opt => opt.Ignore());
            


        }
    }
}
