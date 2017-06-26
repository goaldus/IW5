using System.Linq;
using IWManager.DAL.Entities;
using IWManager.DAL.Entities.Base.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CourseTests
    {
        [TestMethod]
        public void StudentHasOneRatingInCourse()
        {
            var iw5 = new Course();
            var pupil = new Student();
            var project = new GeneralRating {Course = iw5, Title = "1. Projekt", Type = RatingType.projekt, MaxPoints = 20};
            var firstRating = new Rating {GeneralRating = project, Points = 10, Student = pupil};
       
            pupil.Ratings.Add(firstRating);
            iw5.Students.Add(pupil);
            var firstOrDefault = iw5.Students.FirstOrDefault();
            Assert.AreEqual(firstOrDefault.Ratings.Count, 1);
        }

        [TestMethod]
        public void CourseHasFourStudents()
        {
            var pupil1 = new Student { FirstName = "Jan", LastName = "Novák" };
            var pupil2 = new Student { FirstName = "David", LastName = "Černý" };
            var pupil3 = new Student { FirstName = "Jakub", LastName = "Bílý" };
            var pupil4 = new Student { FirstName = "Josef", LastName = "Urostlý" };
            var iw5 = new Course();
            iw5.Students.Add(pupil1);
            iw5.Students.Add(pupil2);
            iw5.Students.Add(pupil3);
            iw5.Students.Add(pupil4);
            Assert.AreEqual(iw5.Students.Count, 4);
        }

    }
}