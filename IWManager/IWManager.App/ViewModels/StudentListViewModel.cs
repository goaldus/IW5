using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.ViewModels
{
    public class StudentListViewModel : ViewModelBase
    {
        private ObservableCollection<StudentListModel> _students;
        private readonly StudentRepository _studentRepository;
        private readonly IMessenger _messenger;

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

        public ICommand SelectStudentCommand { get; }

        public StudentListViewModel(StudentRepository studentRepository, IMessenger messenger)
        {
            _studentRepository = studentRepository;
            _messenger = messenger;

            _messenger.Register<DeletedStudentMessage>(DeletedStudentMessageReceived);
            _messenger.Register<UpdatedStudentMessage>((p) => OnLoad());
            SelectStudentCommand = new RelayCommand(StudentSelectionChanged);
        }

        public void OnLoad()
        {
            Students = new ObservableCollection<StudentListModel>(_studentRepository.GetAll());
        }

        public void StudentSelectionChanged(object parameter)
        {
            var studentId = (StudentListModel)parameter;
            if (studentId == null)
            {
                return;
            }

            _messenger.Send(new SelectedStudentMessage() { Id = studentId.Id });

        }

        private void DeletedStudentMessageReceived(DeletedStudentMessage message)
        {
            var deletedStudent = Students.FirstOrDefault(r => r.Id == message.ObjectId);
            if (deletedStudent != null)
            {
                Students.Remove(deletedStudent);
            }
        }
    }
}