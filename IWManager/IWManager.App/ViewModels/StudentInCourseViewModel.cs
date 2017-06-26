using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels
{
    public class StudentInCourseViewModel : ViewModelBase
    {
        private StudentDetailModel _detail;

        public StudentDetailModel Detail
        {
            get { return _detail; }
            set
            {
                if (Equals(value, Detail))
                    return;

                _detail = value;
                OnPropertyChanged();
            }
        }

        private readonly StudentRepository _studentRepository;
        private readonly RatingRepository _ratingRepository;
        private readonly IMessenger _messenger;

        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public Guid RatingId { get; set; }
        private Guid GeneralRatingId { get; set; }

        public ICommand NewRatingCommand { get; set; }
        public ICommand DeleteRatingCommand { get; }

        public StudentInCourseViewModel(StudentRepository studentRepository, RatingRepository ratingRepository, IMessenger messenger)
        {
            _studentRepository = studentRepository;
            _ratingRepository = ratingRepository;
            _messenger = messenger;

            _messenger.Register<SelectedStudentInCourseMessage>(SelectedStudentInCourse);
            _messenger.Register<SelectedRatingMessage>(SelectedRating);

            NewRatingCommand = new RelayCommand(NewRating);
            DeleteRatingCommand = new RelayCommand(DeleteRating);
        }

        public void SelectedStudentInCourse(SelectedStudentInCourseMessage message)
        {
            CourseId = message.CourseId;
            StudentId = message.StudentId;

            Detail = _studentRepository.GetById(StudentId);
        }

        private void SelectedRating(SelectedRatingMessage message)
        {
            RatingId = message.Id;
            GeneralRatingId = message.GeneralRatingId;
        }

        public void NewRating()
        {
            var ratingDetail = new Views.RatingDetailView();
            _messenger.Send(new NewRatingMessage { CourseId = CourseId, StudentId = StudentId });
            ratingDetail.ShowDialog();
        }

        private void DeleteRating()
        {
            if (RatingId == Guid.Empty)
                return;
            _messenger.Send(new DeleteRatingMessage { RatingId = RatingId, GeneralRatingId = GeneralRatingId, StudentId = StudentId});
            _messenger.Send(new SelectedStudentInCourseMessage { CourseId = CourseId, StudentId = StudentId });
            RatingId = Guid.Empty;
        }
    }
}
