using System.Linq;
using IWManager.DAL.Entities;
using IWManager.DAL.Entities.Base.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void StudentHasTwoRatings()
        {
            var pupil = new Student();
            var project = new GeneralRating {Title = "1. Projekt", Type = RatingType.projekt};
            var exercise = new GeneralRating {Title = "1. Cvičení", Type = RatingType.cvičení};
            var firstRating = new Rating {GeneralRating = project, Points = 10, Student = pupil};
            var secondRating = new Rating {GeneralRating = exercise, Points = 2, Student = pupil};
            pupil.Ratings.Add(firstRating);
            pupil.Ratings.Add(secondRating);
            Assert.AreEqual(pupil.Ratings.Count, 2);
        }

        [TestMethod]
        public void StudentWithZeroRatingsOfTypeExercise()
        {   
            var pupil = new Student();
            var project = new GeneralRating { Title = "1. Projekt", Type = RatingType.projekt };
            var ratingOfProject = new Rating() {GeneralRating = project, Points = 5, Student = pupil};

            int exerciseCount = pupil.Ratings.Count(pupilRating => pupilRating.GeneralRating.Type == RatingType.cvičení);
            Assert.AreEqual(exerciseCount, 0);
        }

        [TestMethod]
        public void StudentAcquired20PointsInSumOfRatings()
        {
            var pupil = new Student();
            var project = new GeneralRating() { Title = "1. Projekt", Type = RatingType.projekt };
            var exercise1 = new GeneralRating() { Title = "1. cvičení", Type = RatingType.cvičení };
            var exercise2 = new GeneralRating() { Title = "2. cvičení", Type = RatingType.cvičení };
            var projectRating = new Rating() { GeneralRating = project, Student = pupil, Points = 10};
            var exerciseRating = new Rating() { GeneralRating = exercise1, Student = pupil, Points = 5};
            var exerciseRating2 = new Rating() { GeneralRating = exercise2, Student = pupil, Points = 5};

            pupil.Ratings.Add(projectRating);
            pupil.Ratings.Add(exerciseRating);
            pupil.Ratings.Add(exerciseRating2);
            int pointsSum = pupil.Ratings.Sum(pupilRating => pupilRating.Points);
            Assert.AreEqual(pointsSum, 20);
        }

        [TestMethod]
        public void StudentCompleted2Exercises()
        {
            var pupil = new Student();
            var project = new GeneralRating() { Title = "1. Projekt", Type = RatingType.projekt };
            var exercise1 = new GeneralRating() { Title = "1. cvičení", Type = RatingType.cvičení };
            var exercise2 = new GeneralRating() { Title = "2. cvičení", Type = RatingType.cvičení };
            var projectRating = new Rating() { GeneralRating = project, Student = pupil, Points = 10 };
            var exerciseRating = new Rating() { GeneralRating = exercise1, Student = pupil, Points = 2 };
            var exerciseRating2 = new Rating() { GeneralRating = exercise2, Student = pupil, Points = 2 };

            pupil.Ratings.Add(projectRating);
            pupil.Ratings.Add(exerciseRating);
            pupil.Ratings.Add(exerciseRating2);
            int exercisesCompleted = pupil.Ratings.Count(pupilRating => pupilRating.GeneralRating.Type == RatingType.cvičení);
            Assert.AreEqual(exercisesCompleted, 2);
        }



    }
}
