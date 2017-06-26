using System;
using System.Windows.Input;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class SaveGeneralRatingCommand : ICommand
    {
        private readonly GeneralRatingRepository _generalRatingRepository;
        private readonly GeneralRatingDetailViewModel _viewModel;
        private readonly IMessenger _messenger;

        public SaveGeneralRatingCommand(GeneralRatingRepository generalRatingRepository, GeneralRatingDetailViewModel viewModel,
            IMessenger messenger)
        {
            _messenger = messenger;
            _generalRatingRepository = generalRatingRepository;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var detail = parameter as GeneralRatingDetailModel;

            if (detail == null) return false;
            if (!string.IsNullOrWhiteSpace(detail.Title) && detail.MaxPoints > 0 && detail.MaxPoints <= 100)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var detail = parameter as GeneralRatingDetailModel;

            if (detail == null)
            {
                return;
            }

            if (detail.Id != Guid.Empty)
            {
                _generalRatingRepository.Update(detail);
            }
            else
            {
                _viewModel.Detail = _generalRatingRepository.Insert(detail, _viewModel.CourseId);
            }

            _messenger.Send(new UpdatedGeneralRatingMessage(detail));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}