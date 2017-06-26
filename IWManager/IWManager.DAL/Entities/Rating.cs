using System.ComponentModel.DataAnnotations;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.DAL.Entities
{
    public class Rating : EntityBase
    {
        [Required]
        public virtual GeneralRating GeneralRating { get; set; }

        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public int Points { get; set; }

        public string Notes { get; set; }
    }
}
