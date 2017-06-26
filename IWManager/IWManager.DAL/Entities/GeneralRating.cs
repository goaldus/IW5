using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.DAL.Entities
{
    public class GeneralRating : EntityBase
    {
        [Required]
        public virtual Course Course { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public RatingType Type { get; set; }

        [Required]
        public int MaxPoints { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    }
}
