using AutoMapper;
using Google.Api;
using Grpc.Core;
using Services.Abstractions;
using Services.Contracts.Course;
using Services.Implementations;
using UtusGrpcService;

namespace UtusGrpcService.GrpcServices
{
    public class CourseGrpcController : UtusGrpcService.CourseGrpcController.CourseGrpcControllerBase
    {
        private readonly ICourseService _service;
        private readonly ILogger<CourseGrpcController> _logger;
        private readonly IMapper _mapper;
        public CourseGrpcController(ICourseService service, ILogger<CourseGrpcController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CreateCourseReply> CreateCourse(CreateCourseRequest request, ServerCallContext context)
        {
            var courseId = await _service.CreateAsync(_mapper.Map<CreateCourseRequest, CreatingCourseDto>(request));
            return new CreateCourseReply()
            {
                Id = courseId
            };
        }
    }
}
