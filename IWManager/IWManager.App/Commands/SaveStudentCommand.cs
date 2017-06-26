using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class SaveStudentCommand : ICommand
    {
        private readonly StudentRepository _studentRepository;
        private readonly StudentDetailViewModel _viewModel;
        private readonly IMessenger _messenger;
        private readonly string _regex = "^x[a-z]{5}\\d{2}$";

        public SaveStudentCommand(StudentRepository studentRepository, StudentDetailViewModel viewModel,
            IMessenger messenger)
        {
            _messenger = messenger;
            _studentRepository = studentRepository;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var detail = parameter as StudentDetailModel;

            if (detail == null) return false;
            if (!string.IsNullOrWhiteSpace(detail.Login))
            {
                if (!string.IsNullOrWhiteSpace(detail.FirstName) && !string.IsNullOrWhiteSpace(detail.LastName)
                    && Regex.Match(detail.Login, _regex).Success)
                {
                    return true;
                }
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var detail = parameter as StudentDetailModel;

            if (detail == null)
            {
                return;
            }

            if (detail.Id != Guid.Empty)
            {
                _studentRepository.Update(detail);
            }
            else
            {
                _viewModel.Detail = _studentRepository.Insert(detail);
            }

            _messenger.Send(new UpdatedStudentMessage(detail));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}