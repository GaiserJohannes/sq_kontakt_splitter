using KontaktSplitter.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace KontaktSplitter.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Contact contactModel;
        private Gender genderModel;
        public RelayCommand SplitCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Contact ContactModel
        {
            get => contactModel;
            set
            {
                contactModel = value;
                OnPropertyChanged(nameof(ContactModel));
            }
        }
        public Gender GenderModel
        {
            get => genderModel;
            set
            {
                genderModel = value;
                OnPropertyChanged(nameof(GenderModel));
            }
        }


        public MainViewModel()
        {
            SplitCommand = new RelayCommand(SplitContact);
            ContactModel = new Contact();
            GenderModel = new Gender();
        }

        private void SplitContact(object obj)
        {

        }




    }
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
