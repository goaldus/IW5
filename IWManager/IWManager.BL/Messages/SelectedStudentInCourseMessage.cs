using System;

namespace IWManager.BL.Messages
{
    public class SelectedStudentInCourseMessage
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
    }
}