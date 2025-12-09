using System;

namespace WebApi.Models.Course
{
    public class CourseCreatedEvent
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
    }
}
