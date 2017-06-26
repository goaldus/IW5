using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Repositories;

namespace IWManager.App
{
    public class ViewModelLocator
    {
        private readonly Messenger _messenger = new Messenger();
        private readonly CourseRepository _courseRepository = new CourseRepository();
        private readonly StudentRepository _studentRepository = new StudentRepository();
        private readonly GeneralRatingRepository _generalRatingRepository = new GeneralRatingRepository();
        private readonly RatingRepository _ratingRepository = new RatingRepository();

        public MainViewModel MainViewModel => CreateMainViewModel();

        private MainViewModel CreateMainViewModel()
        {
            return new MainViewModel(_messenger);
        }

        public CourseListViewModel CourseListViewModel => CreateListViewModel();
        public CourseDetailViewModel CourseDetailViewModel => CreateDetailViewModel();

        private CourseListViewModel CreateListViewModel()
        {
            return new CourseListViewModel(_courseRepository, _messenger);
        }

        private CourseDetailViewModel CreateDetailViewModel()
        {
            return new CourseDetailViewModel(_courseRepository, _messenger);
        }

        public StudentListViewModel StudentListViewModel => CreateStudentListViewModel();
        public StudentDetailViewModel StudentDetailViewModel => CreateStudentDetailViewModel();

        private StudentListViewModel CreateStudentListViewModel()
        {
            return new StudentListViewModel(_studentRepository, _messenger);
        }

        private StudentDetailViewModel CreateStudentDetailViewModel()
        {
            return new StudentDetailViewModel(_studentRepository, _messenger);
        }

        public GeneralRatingListViewModel GeneralRatingListViewModel => CreateGeneralRatingListViewModel();
        public GeneralRatingDetailViewModel GeneralRatingDetailViewModel => CreateGeneralRatingDetailViewModel();

        private GeneralRatingListViewModel CreateGeneralRatingListViewModel()
        {
            return new GeneralRatingListViewModel(_generalRatingRepository, _messenger);
        }

        private GeneralRatingDetailViewModel CreateGeneralRatingDetailViewModel()
        {
            return new GeneralRatingDetailViewModel(_generalRatingRepository, _messenger);
        }

        public StudentsOfCourseViewModel StudentsOfCourseViewModel => CreateStudentsOfCourseViewModel();

        private StudentsOfCourseViewModel CreateStudentsOfCourseViewModel()
        {
            return new StudentsOfCourseViewModel(_studentRepository, _messenger);
        }

        public StudentManagerViewModel StudentManagerViewModel => CreateStudentManagerViewModel();

        private StudentManagerViewModel CreateStudentManagerViewModel()
        {
            return new StudentManagerViewModel(_courseRepository, _studentRepository, _messenger);
        }

        public StudentInCourseViewModel StudentInCourseViewModel => CreateStudentInCourseViewModel();

        private StudentInCourseViewModel CreateStudentInCourseViewModel()
        {
            return new StudentInCourseViewModel(_studentRepository, _ratingRepository, _messenger);
        }

        public RatingListViewModel RatingListViewModel => CreateRatingListViewModel();
        public RatingDetailViewModel RatingDetailViewModel => CreateRatingDetailViewModel();

        private RatingListViewModel CreateRatingListViewModel()
        {
            return new RatingListViewModel(_ratingRepository, _messenger);
        }

        private RatingDetailViewModel CreateRatingDetailViewModel()
        {
            return new RatingDetailViewModel(_generalRatingRepository, _ratingRepository, _messenger);
        }
    }
}