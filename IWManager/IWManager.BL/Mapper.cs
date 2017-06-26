using System;
using System.Linq;
using IWManager.BL.Models;
using IWManager.DAL.Entities;

namespace IWManager.BL
{
    class Mapper
    {
        public CourseListModel MapEntityToListModel(Course entity)
        {
            return new CourseListModel()
            {
                Id = entity.Id,
                Abbreviation = entity.Abbreviation,
                CourseName = entity.CourseName,
                AcademicYear = entity.AcademicYear
            };
        }

        public CourseDetailModel MapEntityToDetailModel(Course entity)
        {
            return new CourseDetailModel()
            {
                Id = entity.Id,
                CourseName = entity.CourseName,
                Abbreviation = entity.Abbreviation,
                AcademicYear = entity.AcademicYear,
                Credits = entity.Credits
            };
        }

        public Course MapDetailModelToEntity(CourseDetailModel model)
        {
            return new Course()
            {
                Id = model.Id,
                CourseName = model.CourseName,
                Abbreviation = model.Abbreviation,
                AcademicYear = model.AcademicYear,
                Credits = model.Credits
            };
        }

        public GeneralRatingListModel MapEntityToListModel(GeneralRating entity)
        {
            return new GeneralRatingListModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Type = entity.Type
            };
        }

        public GeneralRatingDetailModel MapEntityToDetailModel(GeneralRating entity)
        {
            return new GeneralRatingDetailModel()
            {
                Id = entity.Id,
                CourseId = entity.Course.Id,
                Title = entity.Title,
                Type = entity.Type,
                MaxPoints = entity.MaxPoints
            };
        }

        public GeneralRating MapDetailModelToEntity(GeneralRatingDetailModel model)
        {
            return new GeneralRating()
            {
                Id = model.Id,
                Title = model.Title,
                Type = model.Type,
                MaxPoints = model.MaxPoints,
            };
        }

        public StudentListModel MapEntityToListModel(Student entity)
        {
            return new StudentListModel()
            {
                Id = entity.Id,
                Login = entity.Login,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        public StudentDetailModel MapEntityToDetailModel(Student entity)
        {
            return new StudentDetailModel()
            {
                Id = entity.Id,
                Login = entity.Login,
                Birthdate = entity.Birthdate,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Nationality = entity.Nationality,
                PhoneNumber = entity.PhoneNumber,
                Photo = entity.Photo,
                Sex = entity.Sex
            };
        }

        public Student MapDetailModelToEntity(StudentDetailModel entity)
        {
            return new Student()
            {
                Id = entity.Id,
                Login = entity.Login,
                Birthdate = entity.Birthdate,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Nationality = entity.Nationality,
                PhoneNumber = entity.PhoneNumber,
                Photo = entity.Photo,
                Sex = entity.Sex
            };
        }

        public RatingListModel MapEntityToListModel(Rating entity)
        {
            return new RatingListModel()
            {
                Id = entity.Id,
                Title = entity.GeneralRating.Title,
                Type = entity.GeneralRating.Type,
                MaxPoints = entity.GeneralRating.MaxPoints,
                Points = entity.Points
            };
        }

        public RatingDetailModel MapEntityToDetailModel(Rating entity)
        {
            return new RatingDetailModel()
            {
                Id = entity.Id,
                GeneralRatingId = entity.GeneralRating.Id,
                StudentId = entity.Student.Id,
                Title = entity.GeneralRating.Title,
                Type = entity.GeneralRating.Type,
                MaxPoints = entity.GeneralRating.MaxPoints,
                Points = entity.Points,
                Notes = entity.Notes
            };
        }

        public Rating MapDetailModelToEntity(RatingDetailModel model)
        {
            return new Rating()
            {
                Id = model.Id,
                Points = model.Points,
                Notes = model.Notes
            };
        }
    }
}