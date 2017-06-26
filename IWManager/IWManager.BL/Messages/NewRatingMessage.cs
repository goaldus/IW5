using System;

namespace IWManager.BL.Messages
{
    public class NewRatingMessage
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
    }
}