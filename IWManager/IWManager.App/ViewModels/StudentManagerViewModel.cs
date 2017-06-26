using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels
{
    public class StudentManagerViewModel : ViewModelBase
    {
        private ObservableCollection<StudentListModel> _registeredStudents;
        private ObservableCollection<StudentListModel> _otherStudents;

        private readonly CourseRepository _courseRepository;
        private readonly StudentRepository _studentRepository;
        private readonly IMessenger _messenger;

        public Guid CourseId { get; set; }

        public ObservableCollection<StudentListModel> RegisteredStudents
        {
            get { return _registeredStudents; }
            set
            {
                if (Equals(value, _registeredStudents)) return;
                _registeredStudents = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StudentListModel> OtherStudents
        {
            get { return _otherStudents; }
            set
            {
                if (Equals(value, _otherStudents)) return;
                _otherStudents = value;
                OnPropertyChanged();
            }
        }

        public ICollection<StudentListModel> SelectedRegisteredStudents { get; set; }
        public ICollection<StudentListModel> SelectedOtherStudents { get; set; }

        public ICommand AddStudentsCommand { get; }
        public ICommand RemoveStudentsCommand { get; }
        public ICommand SaveCommand { get; }

        public StudentManagerViewModel(CourseRepository courseRepository, StudentRepository studentRepository, IMessenger messenger)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _messenger = messenger;

            _messenger.Register<SelectedCourseInCourseList>(SelectedCourse);

            AddStudentsCommand = new MoveStudentsCommand(AddStudents);
            RemoveStudentsCommand = new MoveStudentsCommand(RemoveStudents);
            SaveCommand = new SaveStudentsInCourseCommand(_courseRepository, this, _messenger);
        }

        public void SelectedCourse(SelectedCourseInCourseList message)
        {
            CourseId = message.Id;
            RegisteredStudents = new ObservableCollection<StudentListModel>(_studentRepository.GetByCourse(message.Id));
            OtherStudents = new ObservableCollection<StudentListModel>(_studentRepository.GetByNotCourse(message.Id));
        }

        public void AddStudents(ICollection<StudentListModel> students)
        {
            foreach (StudentListModel student in students)
            {
                RegisteredStudents.Add(student);
                OtherStudents.Remove(student);
            }
        }

        public void RemoveStudents(ICollection<StudentListModel> students)
        {
            foreach (StudentListModel student in students)
            {
                OtherStudents.Add(student);
                RegisteredStudents.Remove(student);
            }
        }
    }
}
