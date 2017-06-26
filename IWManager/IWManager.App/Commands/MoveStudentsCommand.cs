using System;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IWManager.App.ViewModels;
using IWManager.BL;
using IWManager.BL.Messages;
using IWManager.BL.Models;
using IWManager.BL.Repositories;

namespace IWManager.App.Commands
{
    public class MoveStudentsCommand : ICommand
    {
        private readonly Action<ICollection<StudentListModel>> _action;

        public MoveStudentsCommand(Action<ICollection<StudentListModel>> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            var students = parameter as IEnumerable;
            return students != null;
        }

        public void Execute(object parameter)
        {
            var students = parameter as IEnumerable;
            if (students == null)
                return;

            _action(students.Cast<StudentListModel>().ToList());
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}