using System;

namespace IWManager.BL.Messages
{
    public class DeleteGeneralRatingMessage
    {
        public Guid RatingId { get; set; }
        public Guid CourseId { get; set; }

    }
}