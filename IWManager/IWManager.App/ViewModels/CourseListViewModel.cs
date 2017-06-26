using System;
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
    public class CourseListViewModel : ViewModelBase
    {
        private ObservableCollection<CourseListModel> _courses;
        private readonly CourseRepository _courseRepository;
        private readonly IMessenger _messenger;

        public ObservableCollection<CourseListModel> Courses
        {
            get { return _courses; }
            set
            {
                if (Equals(value, _courses)) return;
                _courses = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectCourseCommand { get; }

        public CourseListViewModel(CourseRepository courseRepository, IMessenger messenger)
        {
            _courseRepository = courseRepository;
            _messenger = messenger;

            _messenger.Register<DeletedCourseMessage>(DeletedCourseMessageReceived);
            _messenger.Register<UpdatedCourseMessage>((p) => OnLoad());
            SelectCourseCommand = new RelayCommand(CourseSelectionChanged);
        }

        public void OnLoad()
        {
            Courses = new ObservableCollection<CourseListModel>(_courseRepository.GetAll());
        }

        public void CourseSelectionChanged(object parameter)
        {
            var courseId = (CourseListModel) parameter;
            if (courseId == null)
            {
                return;
            }

            _messenger.Send(new SelectedCourseMessage() {Id = courseId.Id});
            _messenger.Send(new SelectedCourseInCourseList() { Id = courseId.Id });

        }

        private void DeletedCourseMessageReceived(DeletedCourseMessage message)
        {
            var deletedCourse = Courses.FirstOrDefault(r => r.Id == message.ObjectId);
            if (deletedCourse != null)
            {
                Courses.Remove(deletedCourse);
            }
        }


    }
}