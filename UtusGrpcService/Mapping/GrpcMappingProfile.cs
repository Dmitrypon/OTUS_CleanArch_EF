using AutoMapper;
using Services.Contracts.Course;
using UtusGrpcService;

namespace UtusGrpcService.Mapping
{
    public class GrpcMappingProfile : Profile
    {
        public GrpcMappingProfile()
        {
            CreateMap<CreateCourseRequest, CreatingCourseDto>();

        }
    }
}