using System;
using System.Collections.Generic;
using System.Linq;
using IWManager.BL.Models;
using IWManager.DAL;
using IWManager.DAL.Entities;

namespace IWManager.BL.Repositories
{
    public class GeneralRatingRepository
    {
        private Mapper mapper = new Mapper();

        public List<GeneralRatingListModel> GetAllByCourse(Guid courseId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                return iwManagerDbContext.GeneralRatings
                    .Where(r => r.Course.Id == courseId)
                    .Select(mapper.MapEntityToListModel)
                    .ToList();
            }
        }

        public GeneralRatingDetailModel GetById(Guid id)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var rating = iwManagerDbContext.GeneralRatings
                    .FirstOrDefault(r => r.Id == id);

                return mapper.MapEntityToDetailModel(rating);
            }
        }

        public GeneralRatingDetailModel Insert(GeneralRatingDetailModel detail, Guid courseId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = mapper.MapDetailModelToEntity(detail);
                entity.Id = Guid.NewGuid();
                entity.Course = iwManagerDbContext.Courses.First(c => c.Id == courseId);

                iwManagerDbContext.GeneralRatings.Add(entity);
                iwManagerDbContext.SaveChanges();

                return mapper.MapEntityToDetailModel(entity);
            }
        }

        public void Update(GeneralRatingDetailModel detail)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = iwManagerDbContext.GeneralRatings.First(r => r.Id == detail.Id);
                entity.Course = entity.Course;
                entity.Title = detail.Title;
                entity.Type = detail.Type;
                entity.MaxPoints = detail.MaxPoints;
  
                iwManagerDbContext.SaveChanges();
            }
        }
        
        public void Remove(Guid ratingId, Guid courseId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var course = iwManagerDbContext.Courses.First(c => c.Id == courseId);
                var entity = new GeneralRating() { Id = ratingId, Course = course};
                iwManagerDbContext.GeneralRatings.Attach(entity);

                iwManagerDbContext.GeneralRatings.Remove(entity);
                iwManagerDbContext.SaveChanges();
            }
        }
    }
}
