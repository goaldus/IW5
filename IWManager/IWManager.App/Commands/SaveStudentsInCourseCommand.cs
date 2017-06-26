using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class SaveStudentsInCourseCommand : ICommand
    {
        private readonly CourseRepository _courseRepository;
        private readonly StudentManagerViewModel _viewModel;
        private readonly IMessenger _messenger;

        public SaveStudentsInCourseCommand(CourseRepository courseRepository, StudentManagerViewModel viewModel,
            IMessenger messenger)
        {
            _messenger = messenger;
            _courseRepository = courseRepository;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CourseId == null)
                return false;

            var registeredStudents = parameter as IEnumerable<StudentListModel>;
            return registeredStudents != null;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.CourseId == null)
                return;

                var registeredStudents = parameter as IEnumerable<StudentListModel>;
            if (registeredStudents == null)
                return;

            _courseRepository.UpdateStudents(_viewModel.CourseId, registeredStudents.Select(s => s.Id));

            _messenger.Send(new SelectedCourseInCourseList { Id = _viewModel.CourseId });
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}