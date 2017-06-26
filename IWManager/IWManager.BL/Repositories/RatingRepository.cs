using System;
using System.Collections.Generic;
using System.Linq;
using IWManager.BL.Models;
using IWManager.DAL;
using IWManager.DAL.Entities;

namespace IWManager.BL.Repositories
{
    public class RatingRepository
    {
        private Mapper mapper = new Mapper();

        public List<RatingListModel> GetByCourseAndStudent(Guid courseId, Guid studentId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                return iwManagerDbContext.Ratings
                    .Where(r => r.GeneralRating.Course.Id == courseId && r.Student.Id == studentId)
                    .Select(mapper.MapEntityToListModel)
                    .ToList();
            }
        }

        public RatingDetailModel GetById(Guid id)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var rating = iwManagerDbContext.Ratings
                    .FirstOrDefault(r => r.Id == id);

                return mapper.MapEntityToDetailModel(rating);
            }
        }

        public RatingDetailModel Insert(RatingDetailModel detail, Guid generalRatingId, Guid studentId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = mapper.MapDetailModelToEntity(detail);
                entity.Id = Guid.NewGuid();
                entity.GeneralRating = iwManagerDbContext.GeneralRatings.First(c => c.Id == generalRatingId);
                entity.Student = iwManagerDbContext.Students.First(c => c.Id == studentId);

                iwManagerDbContext.Ratings.Add(entity);
                iwManagerDbContext.SaveChanges();

                return mapper.MapEntityToDetailModel(entity);
            }
        }

        public void Update(RatingDetailModel detail)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = iwManagerDbContext.Ratings.First(r => r.Id == detail.Id);
                
                entity.Points = detail.Points;
                entity.Notes = detail.Notes;

                iwManagerDbContext.SaveChanges();
            }
        }

        public void Remove(Guid ratingId, Guid generalRatingId, Guid studentId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var student = iwManagerDbContext.Students.First(s => s.Id == studentId);
                var generalRating = iwManagerDbContext.GeneralRatings.First(g => g.Id == generalRatingId);
                var entity = new Rating() { Id = ratingId, Student = student, GeneralRating = generalRating};
                iwManagerDbContext.Ratings.Attach(entity);
                
                iwManagerDbContext.Ratings.Remove(entity);
                iwManagerDbContext.SaveChanges();
            }
        }

        public Guid GetGeneralRatingIdByName(string name)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                return iwManagerDbContext.GeneralRatings.First(n => n.Title == name).Id;
            }
        }
    }
}