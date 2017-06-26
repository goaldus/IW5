using System;
using System.Windows.Input;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class NewGeneralRating : ICommand
    {
        private readonly CourseRepository _courseRepository;
        private readonly CourseDetailViewModel _viewModel;
        private readonly IMessenger _messenger;

        public NewGeneralRating(CourseRepository courseRepository, CourseDetailViewModel viewModel,
            IMessenger messenger)
        {
            _messenger = messenger;
            _courseRepository = courseRepository;
            _viewModel = viewModel;
        }


        public bool CanExecute(object parameter)
        {
            return _viewModel.State;
        }

        public void Execute(object parameter)
        {
            var ratingCreator = new Views.GeneralRatingDetailView();
            _messenger.Send(new SelectedCourseInCourseList { Id = _viewModel.Detail.Id });
            _messenger.Send(new NewGeneralRatingMessage());
            ratingCreator.ShowDialog();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}