using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.DAL.Entities
{
    public class Student : EntityBase
    {
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "{0} can have just {1} characters")]
        [RegularExpression(@"^x[a-z]{5}\d{2}$", ErrorMessage = "Wrong format. Type eg. \"xlogin00\".")]
        public string Login { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} can have a max of {1} characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} can have a max of {1} characters")]
        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }
        public string Photo { get; set; }
        public Sex Sex { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "{0} can have just {1} characters")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    }



}
