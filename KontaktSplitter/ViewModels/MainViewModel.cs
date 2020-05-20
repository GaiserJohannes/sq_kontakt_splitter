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
        private ICRMConnector CRMConnector;
        private ILanguageConfiguration Configuration;
        public Gender[] Genders { get; set; }
        public string[] Languages { get; set; }

        private string _selectedTitle;
        public string SelectedTitle
        {
            get => _selectedTitle;
            set
            {
                _selectedTitle = value;
                UpdateLetterSalutCommand?.Execute(this);
                OnPropertyChanged(nameof(SelectedTitle));
            }
        }

        private Function _selectedFunction;
        public Function SelectedFunction 
        { 
            get => _selectedFunction;
            set
            {
                _selectedFunction = value;
                UpdateLetterSalutCommand?.Execute(this);
                OnPropertyChanged(nameof(SelectedFunction));
            }
        }
        public RelayCommand UpdateLetterSalutCommand { get; private set; }
        public RelayCommand SplitContactCommand { get; private set; }
        public RelayCommand AddTitleCommand { get; private set; }
        public RelayCommand SaveContactCommand { get; private set; }
        public RelayCommand CheckDuplicateCommand { get; private set; }
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
                UpdateLetterSalutCommand?.Execute(this);
                OnPropertyChanged(nameof(ContactModel));
            }
        }

        public string LetterSalutation
        {
            get => contactModel.LetterSalutation;
            set
            {
                contactModel.LetterSalutation = value;
                OnPropertyChanged(nameof(LetterSalutation));
            }
        }
               
        public MainViewModel()
        {
            UpdateLetterSalutCommand = new RelayCommand(ExecuteUpdateLetterSalut);
            SplitContactCommand = new RelayCommand(param => this.SplitContact(param));
            ContactModel = new Contact();
            splitter = new DefaultContactSplitter();
            CRMConnector = new CRMMockConnector();
            Configuration = new JSONConfiguration();
            Genders = (Gender[])Enum.GetValues(typeof(Gender));
            Languages = new string[] { "deutsch", "english" };
            OnPropertyChanged("Genders");
            DeleteTitleCommand = new RelayCommand(param => this.DeleteTitle(param));
            AddTitleCommand = new RelayCommand(AddTitle);
            SaveContactCommand = new RelayCommand(SaveContact);
            CheckDuplicateCommand = new RelayCommand(CheckDuplicate);
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
                if (ContactModel.Language.Titles.Remove(SelectedTitle))
                {
                    Configuration.UpdateLanguage(ContactModel.Language);
                }
                ContactModel.Title.Remove(SelectedTitle);
            }
        }

        private void ExecuteUpdateLetterSalut(object obj)
            => LetterSalutation = ContactModel.Language?.CreateLetterSalutation(ContactModel, SelectedFunction);

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
                    if (ContactModel.Language.Titles.Add(vm.Title))
                    {
                        Configuration.UpdateLanguage(ContactModel.Language);
                    }
                }
            }
        }

        private void SaveContact(object obj)
        {
            if (Splitted)
            {
                CRMConnector.StoreContact(contactModel);
            }
            else
            {
                MessageBox.Show("Bitte gebe einen Kontakt ein, um diesen speichern zu können.", "Achtung");
            }
        }

        private void CheckDuplicate(object obj)
        {
            if (Splitted)
            {
                if (CRMConnector.ContainsContact(contactModel))
                {
                    MessageBox.Show("Kontakt ist bereits im CRM-System vorhanden", "CRM-System");
                }
                else
                {
                    MessageBox.Show("Kontakt ist noch nicht im CRM-System vorhanden", "CRM-System");
                }
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
