using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IWManager.BL.Models;
using IWManager.DAL;
using IWManager.DAL.Entities;

namespace IWManager.BL.Repositories
{
    public class StudentRepository
    {
        private Mapper mapper = new Mapper();

        public List<StudentListModel> GetAll()
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                return iwManagerDbContext.Students
                    .Select(mapper.MapEntityToListModel)
                    .ToList();
            }
        }

        public List<StudentListModel> GetByCourse(Guid courseId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                return iwManagerDbContext.Students
                    .Where(s => s.Courses.Any(c => c.Id == courseId))
                    .Select(mapper.MapEntityToListModel)
                    .ToList();
            }
        }

        public List<StudentListModel> GetByNotCourse(Guid courseId)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                return iwManagerDbContext.Students
                    .Where(s => !s.Courses.Any(c => c.Id == courseId))
                    .Select(mapper.MapEntityToListModel)
                    .ToList();
            }
        }

        public StudentDetailModel GetById(Guid id)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var studentEntity = iwManagerDbContext.Students
                    .Include(r => r.Ratings)
                    .FirstOrDefault(r => r.Id == id);

                return mapper.MapEntityToDetailModel(studentEntity);
            }
        }

        public StudentDetailModel Insert(StudentDetailModel detail)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = mapper.MapDetailModelToEntity(detail);
                entity.Id = Guid.NewGuid();

                iwManagerDbContext.Students.Add(entity);
                iwManagerDbContext.SaveChanges();

                return mapper.MapEntityToDetailModel(entity);
            }
        }

        public void Update(StudentDetailModel detail)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = iwManagerDbContext.Students.First(r => r.Id == detail.Id);

                entity.Login = detail.Login;
                entity.FirstName = detail.FirstName;
                entity.LastName = detail.LastName;
                entity.Email = detail.Email;
                entity.Nationality = detail.Nationality;
                entity.Photo = detail.Photo;
                entity.Sex = detail.Sex;
                entity.Birthdate = detail.Birthdate;
                entity.PhoneNumber = detail.PhoneNumber;

                iwManagerDbContext.SaveChanges();
            }
        }

        public void Remove(Guid id)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = new Student() { Id = id };
                iwManagerDbContext.Students.Attach(entity);

                iwManagerDbContext.Students.Remove(entity);
                iwManagerDbContext.SaveChanges();
            }
        }
    }
}