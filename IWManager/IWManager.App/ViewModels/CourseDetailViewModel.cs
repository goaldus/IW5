using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels
{
    public class CourseDetailViewModel : ViewModelBase
    {
        private readonly CourseRepository _courseRepository;
        private readonly IMessenger _messenger;
        private CourseDetailModel _detail;

        public CourseDetailModel Detail
        {
            get { return _detail;}
            set
            {
                if (Equals(value, Detail))
                    return;

                _detail = value;
                OnPropertyChanged();
            }
        }

        private Guid RatingId { get; set; }
        private Guid StudentId { get; set; }
        public bool State { get; set; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ShowStudentManagerCommand { get; }
        public ICommand NewGeneralRatingCommand { get; }
        public ICommand EditGeneralRatingCommand { get; }
        public ICommand DeleteGeneralRatingCommand { get; }
        public ICommand ShowStudentDetailCommand { get; }

        public CourseDetailViewModel(CourseRepository courseRepository, IMessenger messenger)
        {
            _courseRepository = courseRepository;
            _messenger = messenger;
            ShowStudentManagerCommand = new ShowStudentManager(courseRepository, this, messenger);
            ShowStudentDetailCommand = new RelayCommand(ShowStudentDetail);
            NewGeneralRatingCommand = new NewGeneralRating(courseRepository, this, messenger);
            EditGeneralRatingCommand = new RelayCommand(EditGeneralRating);
            DeleteGeneralRatingCommand = new RelayCommand(DeleteGeneralRating);
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new SaveCourseCommand(courseRepository, this, messenger);

            _messenger.Register<SelectedCourseMessage>(SelectedCourse);
            _messenger.Register<NewCourseMessage>(NewCourseMessageReceived);
            _messenger.Register<SelectedGeneralRatingMessage>(SelectedRating);
            _messenger.Register<SelectedStudentInCourseViewMessage>(SelectedStudent);
        }

        private void ShowStudentManager()
        {
            var manager = new Views.StudentManagerView();
            _messenger.Send(new SelectedCourseInCourseList { Id = Detail.Id });
            manager.ShowDialog();
        }

        private void ShowStudentDetail()
        {
            if (StudentId == Guid.Empty)
                return;

            var studentDetail = new Views.StudentInCourseView();
            _messenger.Send(new SelectedStudentInCourseMessage { CourseId = Detail.Id, StudentId = StudentId });
            studentDetail.ShowDialog();
        }

        private void SelectedStudent(SelectedStudentInCourseViewMessage message)
        {
            StudentId = message.Id;
        }

        private void NewGeneralRating()
        {
            var ratingCreator = new Views.GeneralRatingDetailView();
            _messenger.Send(new SelectedCourseInCourseList { Id = Detail.Id });
            _messenger.Send(new NewGeneralRatingMessage());
            ratingCreator.ShowDialog();
        }

        private void EditGeneralRating()
        {
            var rating = new Views.GeneralRatingDetailView {Window = {Title = "Editovat hodnocení"}};
            if (RatingId == Guid.Empty)
                return;
            _messenger.Send(new EditGeneralRatingMessage { Id = RatingId });
            rating.ShowDialog();
        }

        private void DeleteGeneralRating()
        {
            if (RatingId == Guid.Empty)
                return;
            _messenger.Send(new DeleteGeneralRatingMessage { RatingId = RatingId, CourseId = Detail.Id});
        }

        private void SelectedRating(SelectedGeneralRatingMessage message)
        {
            RatingId = message.Id;
        }

        private void Delete()
        {
            if (IsSavedCourse())
            {
                var result = MessageBox.Show("Opravdu chcete smazat předmět ?", "Smazání předmětu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                _courseRepository.Remove(Detail.Id);
                _messenger.Send(new DeletedCourseMessage(Detail.Id));
            }

            Detail = null;
        }

        private bool IsSavedCourse()
        {
            return Detail.Id != Guid.Empty;
        }

        private void SelectedCourse(SelectedCourseMessage message)
        {
            Detail = _courseRepository.GetById(message.Id);
            State = true;
            RatingId = Guid.Empty;
            StudentId = Guid.Empty;
        }

        private void NewCourseMessageReceived(NewCourseMessage message)
        {
            Detail = new CourseDetailModel();
            State = false;
        }

        public IEnumerable<int> SelectableAcademicYears
        {
            get
            {
                int actualYear = DateTime.Now.Year;
                for (var y = actualYear; y <= actualYear + 5; y++)
                    yield return y;
            }
        }
    }
}