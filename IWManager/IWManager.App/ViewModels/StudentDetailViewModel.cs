using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels
{
    public class StudentDetailViewModel : ViewModelBase
    {
        private readonly StudentRepository _studentRepository;
        private readonly IMessenger _messenger;
        private StudentDetailModel _detail;

        public StudentDetailModel Detail
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

        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }

        public StudentDetailViewModel(StudentRepository studentRepository, IMessenger messenger)
        {
            _studentRepository = studentRepository;
            _messenger = messenger;
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new SaveStudentCommand(studentRepository, this, messenger);

            _messenger.Register<SelectedStudentMessage>(SelectedStudent);
            _messenger.Register<NewStudentMessage>(NewStudentMessageReceived);

        }

        public void Delete()
        {
            if (IsSavedStudent())
            {
                MessageBoxResult result = MessageBox.Show("Opravdu chcete smazat studenta ?", "Smazání studenta", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                _studentRepository.Remove(Detail.Id);
                _messenger.Send(new DeletedStudentMessage(Detail.Id));
            }

            Detail = null;
        }

        private bool IsSavedStudent()
        {
            return Detail.Id != Guid.Empty;
        }

        private void SelectedStudent(SelectedStudentMessage message)
        {
            Detail = _studentRepository.GetById(message.Id);
        }

        private void NewStudentMessageReceived(NewStudentMessage message)
        {
            Detail = new StudentDetailModel();
        }

    }
}