using System;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.BL.Models
{
    public class RatingListModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public RatingType Type { get; set; }
        public int MaxPoints { get; set; }
        public int Points { get; set; }
    }
}
