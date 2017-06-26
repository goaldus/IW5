using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels 
{
    public class StudentsOfCourseViewModel : ViewModelBase
    {
        private ObservableCollection<StudentListModel> _students;
        private readonly StudentRepository _studentRepository;
        private readonly IMessenger _messenger;

        public Guid CourseId { get; set; }
        public ICommand SelectStudentCommand { get; }

        public ObservableCollection<StudentListModel> Students
        {
            get { return _students; }
            set
            {
                if (Equals(value, _students)) return;
                _students = value;
                OnPropertyChanged();
            }
        }

        public StudentsOfCourseViewModel(StudentRepository studentRepository, IMessenger messenger)
        {
            _studentRepository = studentRepository;
            _messenger = messenger;

            SelectStudentCommand = new RelayCommand(StudentSelectionChanged);

            _messenger.Register<SelectedCourseInCourseList>(OnLoad);
            _messenger.Register<NewCourseMessage>((p) => EmptyList());
        }

        private void StudentSelectionChanged(object parameter)
        {
            var student = (StudentListModel)parameter;
            if (student == null)
            {
                return;
            }
            _messenger.Send(new SelectedStudentInCourseViewMessage { Id = student.Id });
        }

        private void OnLoad(SelectedCourseInCourseList message)
        {
            CourseId = message.Id;
            Students = new ObservableCollection<StudentListModel>(_studentRepository.GetByCourse(message.Id));
        }

        private void EmptyList()
        {
            Students = null;
        }



    }
}