using System;
using System.Data.Entity.Migrations;
using System.Linq;
using IWManager.DAL.Entities;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IWManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IWManagerDbContext context)
        {
            var peter = new Student()
            {
                Id = new Guid("5abdfee1-c970-4afd-aff8-aa3cfef8b1ac"),
                FirstName = "Petr",
                LastName = "Novák",
                Login = "xnovak01",
                Birthdate = new DateTime(1993, 6, 16)
            };

            var richard = new Student()
            {
                Id = new Guid("012ac89a-94e3-4bc2-94b5-c9b05fc83375"),
                FirstName = "Richard",
                LastName = "Brusle",
                Login = "xbrusl05",
                Birthdate = new DateTime(1995, 7, 11)
            };

            context.Students.AddOrUpdate(i => i.Id, peter, richard);
            context.SaveChanges();

            peter = context.Students.First(s => s.Id == peter.Id);
            richard = context.Students.First(s => s.Id == richard.Id);

            var IW1 = new Course()
            {
                Id = new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843"),
                Abbreviation = "IW1",
                CourseName = "Hokus pokus",
                AcademicYear = 2018,
                Credits = 5,
                Students = {peter, richard}
            };

            context.Courses.AddOrUpdate(i => i.Id, IW1);
            context.SaveChanges();

            IW1 = context.Courses.First(s => s.Id == IW1.Id);

            var projekt = new GeneralRating()
            {
                Course = IW1,
                Id = new Guid("41ba4c9a-f5bc-48b7-ab54-9dd14ae76790"),
                Title = "PROJEKT",
                Type = RatingType.projekt,
                MaxPoints = 10
            };

            context.GeneralRatings.AddOrUpdate(r => r.Id, projekt);

            context.SaveChanges();
        }
    }
}
