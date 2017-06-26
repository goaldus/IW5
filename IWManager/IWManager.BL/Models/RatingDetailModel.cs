using System;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.BL.Models
{
    public class RatingDetailModel
    {
        public Guid Id { get; set; }
        public Guid GeneralRatingId { get; set; }
        public Guid StudentId { get; set; }
        public string Title { get; set; }
        public RatingType Type { get; set; }
        public int MaxPoints { get; set; }
        public int Points { get; set; }
        public string Notes { get; set; }
    }
}
