using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.DAL.Entities
{
    public class Course : EntityBase
    {
        [Required]
        public string CourseName { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "{0} can have just {1} characters")]
        public string Abbreviation { get; set; }

        [Required]
        public int AcademicYear { get; set; }

        public int Credits { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<GeneralRating> Ratings { get; set; } = new List<GeneralRating>();
    }
}