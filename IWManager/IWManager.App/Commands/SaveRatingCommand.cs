using System;
using System.Windows.Input;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class SaveRatingCommand : ICommand
    {
        private readonly RatingRepository _ratingRepository;
        private readonly RatingDetailViewModel _viewModel;
        private readonly IMessenger _messenger;

        public SaveRatingCommand(RatingRepository ratingRepository,RatingDetailViewModel viewModel,
            IMessenger messenger)
        {
            _messenger = messenger;
            _ratingRepository = ratingRepository;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var detail = parameter as RatingDetailModel;
            if (detail == null) return false;
            if (detail.GeneralRatingId != Guid.Empty && detail.Points >= 0 && detail.Points <= 100)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var detail = parameter as RatingDetailModel;

            if (detail == null)
            {
                return;
            }

            if (detail.Id != Guid.Empty)
            {
                _ratingRepository.Update(detail);
            }
            else
            {
                _viewModel.Detail = _ratingRepository.Insert(detail, detail.GeneralRatingId, _viewModel.StudentId);
            }

            _messenger.Send(new SelectedStudentInCourseMessage { CourseId = _viewModel.CourseId, StudentId = _viewModel.StudentId });
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}