using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels
{
    public class RatingListViewModel : ViewModelBase
    {
        private ObservableCollection<RatingListModel> _ratings;
        private readonly RatingRepository _ratingRepository;
        private readonly IMessenger _messenger;

        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }

        public ICommand SelectRatingCommand { get; }

        public ObservableCollection<RatingListModel> Ratings
        {
            get { return _ratings; }
            set
            {
                if (Equals(value, _ratings)) return;
                _ratings = value;
                OnPropertyChanged();
            }
        }

        public RatingListViewModel(RatingRepository ratingRepository, IMessenger messenger)
        {
            _ratingRepository = ratingRepository;
            _messenger = messenger;

            SelectRatingCommand = new RelayCommand(RatingSelectionChanged);
            
            _messenger.Register<SelectedStudentInCourseMessage>(SelectRatings);
            _messenger.Register<DeleteRatingMessage>(DeleteRating);
        }

        private void SelectRatings(SelectedStudentInCourseMessage message)
        {
            Ratings = new ObservableCollection<RatingListModel>(_ratingRepository.GetByCourseAndStudent(message.CourseId, message.StudentId));
        }

        private void RatingSelectionChanged(object parameter)
        {
            var rating = (RatingListModel)parameter;
            if (rating == null)
            {
                return;
            }
            var generalRatingId =_ratingRepository.GetGeneralRatingIdByName(rating.Title);
            _messenger.Send(new SelectedRatingMessage() { Id = rating.Id, GeneralRatingId = generalRatingId});
        }

        private void DeleteRating(DeleteRatingMessage message)
        {
            var result = MessageBox.Show("Opravdu chcete smazat hodnocení ?", "Smazání hodnocení", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            _ratingRepository.Remove(message.RatingId, message.GeneralRatingId, message.StudentId);
        }

    }
}