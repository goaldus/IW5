using System;
using System.Windows.Input;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class ShowStudentManager : ICommand
    {
        private readonly CourseRepository _courseRepository;
        private readonly CourseDetailViewModel _viewModel;
        private readonly IMessenger _messenger;

        public ShowStudentManager(CourseRepository courseRepository, CourseDetailViewModel viewModel,
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
            var manager = new Views.StudentManagerView();
            _messenger.Send(new SelectedCourseInCourseList { Id = _viewModel.Detail.Id });
            manager.ShowDialog();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}