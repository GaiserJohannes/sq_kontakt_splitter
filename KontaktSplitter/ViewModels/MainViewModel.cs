using KontaktSplitter.Lang;
using KontaktSplitter.Model;
using KontaktSplitter.Services;
using KontaktSplitter.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace KontaktSplitter.ViewModels
{
    /// <summary>
    /// ViewModel for MainWindow
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private Contact contactModel;
        private bool Splitted = false;
        private IContactSplitter splitter;
        public Gender[] Genders { get; set; }
        public string SelectedTitle { get; set; }
        public Function SelectedFunction { get; set; }
        public RelayCommand SplitContactCommand { get; private set; }
        public RelayCommand AddTitleCommand { get; private set; }
        public RelayCommand SaveContactCommand { get; private set; }
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
                ContactModel.LetterSalutation = contactModel.Language?.CreateLetterSalutation(contactModel, SelectedTitle);
                OnPropertyChanged(nameof(ContactModel));
            }
        }

        public MainViewModel()
        {
            SplitContactCommand = new RelayCommand(param => this.SplitContact(param));
            ContactModel = new Contact();
            splitter = new DefaultContactSplitter();
            Genders = (Gender[])Enum.GetValues(typeof(Gender));
            OnPropertyChanged("Genders");
            DeleteTitleCommand = new RelayCommand(param => this.DeleteTitle(param));
            AddTitleCommand = new RelayCommand(AddTitle);
            SaveContactCommand = new RelayCommand(SaveContact);
        }

        private void SplitContact(object obj)
        {
            var text = obj as string;
            if (text != "")
            {
               ContactModel= splitter.SplitContact(text);
                if (ContactModel.Gender == Gender.UNKNOWN)
                {
                    MessageBox.Show("Die Sprache ist unbekannt! Bitte stellen sie das Geschlecht und die korrekte Sprache ein", "Sprache unbekannt");
                }
                Splitted = true;
            }
        }

        private void DeleteTitle(object obj)
        {
            if (SelectedTitle != null)
            {
                ContactModel.Title.Remove(SelectedTitle);
            }
        }

        private void AddTitle(object obj)
        {
            if (Splitted)
            {
                AddTitleViewModel vm = new AddTitleViewModel();
                AddTitleWindow window = new AddTitleWindow(vm);
                var result = window.ShowDialog();
                if (result != null && result.Value)
                {
                    ContactModel.Title.Add(vm.Title);
                }
            }
        }

        private void SaveContact(object obj)
        {
            if (Splitted)
            {

            }
        }

    }
    /// <summary>
    /// Class for Command-Actions
    /// </summary>
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
