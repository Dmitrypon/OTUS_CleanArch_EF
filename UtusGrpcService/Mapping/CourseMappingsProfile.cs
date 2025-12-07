using AutoMapper;
using Services.Contracts.Course;
using UtusGrpcService.Models.Course;

namespace UtusGrpcService.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности курса.
    /// </summary>
    public class CourseMappingsProfile : Profile
    {
        public CourseMappingsProfile()
        {
            CreateMap<CourseDto, CourseModel>();
            CreateMap<CreatingCourseModel, CreatingCourseDto>();
            CreateMap<UpdatingCourseModel, UpdatingCourseDto>();
            CreateMap<CourseFilterModel, CourseFilterDto>();
            CreateMap<UpdatingCourseWithLessonsModel, UpdatingCourseWithLessonsDto>();
        }
    }
}
