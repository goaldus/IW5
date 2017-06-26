using System.Windows.Input;
using IWManager.App.Commands;
using IWManager.BL;
using IWManager.BL.Messages;

namespace IWManager.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;

        public ICommand CreateCourseCommand { get; }
        public ICommand CreateStudentCommand { get; }

        public MainViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            CreateCourseCommand = new RelayCommand(() => _messenger.Send(new NewCourseMessage()));
            CreateStudentCommand = new RelayCommand(() => _messenger.Send(new NewStudentMessage()));
        }
    }
}
