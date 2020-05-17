using KontaktSplitter.Model;
using KontaktSplitter.Views;
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
        public string[] Genders { get; set; }
        public string SelectedTitle { get; set; }
        public RelayCommand SplitContactCommand { get; private set; }
        public RelayCommand AddTitleCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand DeleteTitleCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
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

        public MainViewModel()
        {
            SplitContactCommand = new RelayCommand(param=> this.SplitContact(param));
            ContactModel = new Contact();
            Genders = Enum.GetNames(typeof(Gender));
            OnPropertyChanged("Genders");
            DeleteTitleCommand = new RelayCommand(param => this.DeleteTitle(param));
            AddTitleCommand = new RelayCommand(AddTitle);
        }

        private void SplitContact(object obj)
        {
            var text = obj as string;

        }

        private void DeleteTitle(object obj)
        {
            ContactModel.Title.Remove(SelectedTitle);
        }

        private void AddTitle(object obj)
        {
            AddTitleViewModel vm = new AddTitleViewModel();
            AddTitleWindow window = new AddTitleWindow(vm);
            var result = window.ShowDialog();
            if (result!=null && result.Value)
            {
                ContactModel.Title.Add(vm.Title);
            }
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
