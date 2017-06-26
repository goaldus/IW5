using System;
using System.Windows.Input;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class SaveCourseCommand : ICommand
    {
        private readonly CourseRepository _courseRepository;
        private readonly CourseDetailViewModel _viewModel;
        private readonly IMessenger _messenger;

        public SaveCourseCommand(CourseRepository courseRepository, CourseDetailViewModel viewModel,
            IMessenger messenger)
        {
            _messenger = messenger;
            _courseRepository = courseRepository;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var detail = parameter as CourseDetailModel;

            if (detail == null) return false;
            if (string.IsNullOrWhiteSpace(detail.Abbreviation))
            {
                return false;
            }
            else
            {
                if (detail.Abbreviation.Length == 3 && !string.IsNullOrWhiteSpace(detail.CourseName) 
                    && detail.AcademicYear != 0 && detail.Credits >= 0 && detail.Credits < 16)
                {
                    return true;
                }
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var detail = parameter as CourseDetailModel;

            if (detail == null)
            {
                return;
            }

            if (detail.Id != Guid.Empty)
            {
                _courseRepository.Update(detail);
            }
            else
            {
                _viewModel.Detail = _courseRepository.Insert(detail);
            }

            _messenger.Send(new UpdatedCourseMessage(detail));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}