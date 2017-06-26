using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels
{
    public class RatingDetailViewModel : ViewModelBase
    {
        private readonly GeneralRatingRepository _generalRatingRepository;
        private readonly RatingRepository _ratingRepository;
        private readonly IMessenger _messenger;
        private RatingDetailModel _detail;

        public RatingDetailModel Detail
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

        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public ICommand SaveCommand { get; }

        public IList<GeneralRatingListModel> GeneralRatings { get; set; }

        public RatingDetailViewModel(GeneralRatingRepository generalRatingRepository, RatingRepository ratingRepository, IMessenger messenger)
        {
            _generalRatingRepository = generalRatingRepository;
            _ratingRepository = ratingRepository;
            _messenger = messenger;
            SaveCommand = new SaveRatingCommand(ratingRepository, this, messenger);
            
            _messenger.Register<NewRatingMessage>(NewRatingMessageReceived);
        }

        private void UpdateGeneralRatings()
        {
            GeneralRatings = _generalRatingRepository.GetAllByCourse(CourseId);
            OnPropertyChanged(nameof(GeneralRatings));
        }

        private void NewRatingMessageReceived(NewRatingMessage message)
        {
            CourseId = message.CourseId;
            StudentId = message.StudentId;

            Detail = new RatingDetailModel();

            UpdateGeneralRatings();
        }

    }
}
