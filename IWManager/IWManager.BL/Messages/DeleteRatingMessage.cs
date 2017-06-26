using System;

namespace IWManager.BL.Messages
{
    public class DeleteRatingMessage
    {
        public Guid RatingId { get; set; }
        public Guid GeneralRatingId { get; set; }
        public Guid StudentId { get; set; }
    }
}