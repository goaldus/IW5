using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IWManager.BL.Models;
using IWManager.DAL;
using IWManager.DAL.Entities;

namespace IWManager.BL.Repositories
{
    public class CourseRepository
    {
        private Mapper mapper = new Mapper();

        public List<CourseListModel> GetAll()
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                return iwManagerDbContext.Courses
                    .Select(mapper.MapEntityToListModel)
                    .ToList();
            }
        }

        public CourseDetailModel GetById(Guid id)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var course = iwManagerDbContext.Courses
                    .Include(r => r.Students)
                    .FirstOrDefault(r => r.Id == id);

                return mapper.MapEntityToDetailModel(course);
            }
        }


        public CourseDetailModel Insert(CourseDetailModel detail)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = mapper.MapDetailModelToEntity(detail);
                entity.Id = Guid.NewGuid();

                iwManagerDbContext.Courses.Add(entity);
                iwManagerDbContext.SaveChanges();

                return mapper.MapEntityToDetailModel(entity);
            }
        }

        public void Update(CourseDetailModel detail)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = iwManagerDbContext.Courses.First(r => r.Id == detail.Id);

                entity.Abbreviation = detail.Abbreviation;
                entity.CourseName = detail.CourseName;
                entity.AcademicYear = detail.AcademicYear;
                entity.Credits = detail.Credits;

                iwManagerDbContext.SaveChanges();
            }
        }

        public void UpdateStudents(Guid courseId, IEnumerable<Guid> students)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = iwManagerDbContext.Courses.First(r => r.Id == courseId);

                entity.Students.Clear();
                foreach (Student student in students.Select(id => iwManagerDbContext.Students.First(s => s.Id == id)))
                    entity.Students.Add(student);

                iwManagerDbContext.SaveChanges();
            }
        }

        public void Remove(Guid id)
        {
            using (var iwManagerDbContext = new IWManagerDbContext())
            {
                var entity = new Course() { Id = id };
                iwManagerDbContext.Courses.Attach(entity);

                iwManagerDbContext.Courses.Remove(entity);
                iwManagerDbContext.SaveChanges();
            }
        }
    }
}