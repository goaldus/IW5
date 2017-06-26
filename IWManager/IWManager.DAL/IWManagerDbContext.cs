using System.Data.Entity;
using IWManager.DAL.Entities;

namespace IWManager.DAL
{
    public class IWManagerDbContext : DbContext
    {
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<Student> Students { get; set; }
        public IDbSet<GeneralRating> GeneralRatings { get; set; }
        public IDbSet<Rating> Ratings { get; set; }

        public IWManagerDbContext() : base("IWManagerContext")
        {
            
        }
    }
}
