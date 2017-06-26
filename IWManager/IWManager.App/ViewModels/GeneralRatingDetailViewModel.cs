using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.App.ViewModels
{
    public class GeneralRatingDetailViewModel : ViewModelBase
    {
        private readonly GeneralRatingRepository _generalRatingRepository;
        private readonly IMessenger _messenger;
        private GeneralRatingDetailModel _detail;

        public GeneralRatingDetailModel Detail
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
        public ICommand SaveCommand { get; }
        public IList<RatingType> RatingTypes => Enum.GetValues(typeof(RatingType)).Cast<RatingType>().ToList();

        public GeneralRatingDetailViewModel(GeneralRatingRepository generalRatingRepository, IMessenger messenger)
        {
            _generalRatingRepository = generalRatingRepository;
            _messenger = messenger;
            SaveCommand = new SaveGeneralRatingCommand(generalRatingRepository, this, messenger);
            
            _messenger.Register<SelectedCourseInCourseList>(OnLoad);
            _messenger.Register<NewGeneralRatingMessage>(NewGeneralRatingMessageReceived);
            _messenger.Register<EditGeneralRatingMessage>(EditGeneralRatingMessageReceived);
        }

        private void OnLoad(SelectedCourseInCourseList message)
        {
            CourseId = message.Id;
        }

        private void NewGeneralRatingMessageReceived(NewGeneralRatingMessage message)
        {
            Detail = new GeneralRatingDetailModel();
        }

        private void EditGeneralRatingMessageReceived(EditGeneralRatingMessage message)
        {
            Detail = _generalRatingRepository.GetById(message.Id);
            
        }

    }
}
