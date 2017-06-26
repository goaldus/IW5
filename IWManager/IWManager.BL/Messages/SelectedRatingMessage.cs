using System;

namespace IWManager.BL.Messages
{
    public class SelectedRatingMessage
    {
        public Guid Id { get; set; }
        public Guid GeneralRatingId { get; set; }
    }
}