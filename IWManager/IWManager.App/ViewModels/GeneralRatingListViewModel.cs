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
    public class GeneralRatingListViewModel : ViewModelBase
    {
        private ObservableCollection<GeneralRatingListModel> _generalRatings;
        private readonly GeneralRatingRepository _generalRatingRepository;
        private readonly IMessenger _messenger;

        public Guid CourseId { get; set; }
        public ICommand SelectGeneralRatingCommand { get; }

        public ObservableCollection<GeneralRatingListModel> GeneralRatings
        {
            get { return _generalRatings; }
            set
            {
                if (Equals(value, _generalRatings)) return;
                _generalRatings = value;
                OnPropertyChanged();
            }
        }

        public GeneralRatingListViewModel(GeneralRatingRepository generalRatingRepository, IMessenger messenger)
        {
            _generalRatingRepository = generalRatingRepository;
            _messenger = messenger;

            SelectGeneralRatingCommand = new RelayCommand(GeneralRatingSelectionChanged);

            _messenger.Register<SelectedCourseInCourseList>(OnLoad);
            _messenger.Register<UpdatedGeneralRatingMessage>((p) => Reload());
            _messenger.Register<DeleteGeneralRatingMessage>(DeleteGeneralRating);
            _messenger.Register<NewCourseMessage>((p) => EmptyList());
        }

        private void EmptyList()
        {
            GeneralRatings = null;
        }

        private void OnLoad(SelectedCourseInCourseList message)
        {
            CourseId = message.Id;
            GeneralRatings = new ObservableCollection<GeneralRatingListModel>(_generalRatingRepository.GetAllByCourse(message.Id));
        }

        private void Reload()
        {
            GeneralRatings = new ObservableCollection<GeneralRatingListModel>(_generalRatingRepository.GetAllByCourse(CourseId));
        }

        private void GeneralRatingSelectionChanged(object parameter)
        {
            var rating = (GeneralRatingListModel)parameter;
            if (rating == null)
            {
                return;
            }
            _messenger.Send(new SelectedGeneralRatingMessage() { Id = rating.Id });
        }

        private void DeleteGeneralRating(DeleteGeneralRatingMessage message)
        {
            var result = MessageBox.Show("Opravdu chcete smazat hodnocení ?", "Smazání hodnocení", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            _generalRatingRepository.Remove(message.RatingId, message.CourseId);

            var rating = GeneralRatings.FirstOrDefault(r => r.Id == message.RatingId);
            if (rating != null)
            {
                GeneralRatings.Remove(rating);
            }
        }



    }
}