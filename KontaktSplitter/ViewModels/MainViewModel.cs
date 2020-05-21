using GongSolutions.Wpf.DragDrop;
using KontaktSplitter.Lang;
using KontaktSplitter.Model;
using KontaktSplitter.Services;
using KontaktSplitter.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private readonly IContactSplitter splitter;
        private readonly ICRMConnector CRMConnector;
        private readonly ILanguageConfiguration Configuration;
        private Function _selectedFunction;
        private string _selectedTitle;

        #region Properties
        public Gender[] Genders { get; set; }
        public string[] Languages { get; set; }
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

        public Contact ContactModel
        {
            get => contactModel;
            set
            {
                contactModel = value;
                UpdateLetterSalutCommand?.Execute(this);
                OnPropertyChanged(nameof(ContactModel));
                OnPropertyChanged(nameof(ContactSalutation));
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

        public string ContactSalutation
        {
            get => contactModel.Salutation;
            set
            {
                contactModel.Salutation = value;
                OnPropertyChanged(nameof(ContactSalutation));
            }
        }

        public RelayCommand UpdateLetterSalutCommand { get; private set; }
        public RelayCommand SplitContactCommand { get; private set; }
        public RelayCommand AddTitleCommand { get; private set; }
        public RelayCommand ChangeGenderCommand { get; private set; }
        public RelayCommand SaveContactCommand { get; private set; }
        public RelayCommand CheckDuplicateCommand { get; private set; }
        public RelayCommand DeleteTitleCommand { get; private set; }
        public RelayCommand ClearFunctionsCommand { get; private set; }
        #endregion

        #region PropertyChange-Event
        /// <summary>
        /// Raise an event, if a property's value changed 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        /// <summary>
        /// Contructor
        /// </summary>
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
            OnPropertyChanged(nameof(Genders));
            DeleteTitleCommand = new RelayCommand(param => this.DeleteTitle(param));
            AddTitleCommand = new RelayCommand(AddTitle);
            ChangeGenderCommand = new RelayCommand(ChangeGender);
            SaveContactCommand = new RelayCommand(SaveContact);
            CheckDuplicateCommand = new RelayCommand(CheckDuplicate);
            ClearFunctionsCommand = new RelayCommand(ClearFunction);
        }

        /// <summary>
        /// Splits the Contact (Split-Button clicked)
        /// </summary>
        /// <param name="obj">Input Text</param>
        private void SplitContact(object obj)
        {
            var text = obj as string;
            if (text != "")
            {
                ContactModel = splitter.SplitContact(text);
                if (ContactModel.Gender == Gender.UNKNOWN)
                {
                    MessageBox.Show("Die Sprache ist unbekannt! Bitte stellen sie das Geschlecht und die korrekte Sprache ein", "Sprache unbekannt");
                }
                Splitted = true;
            }
        }
        /// <summary>
        /// Deletes a Title (Title-List Item)
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Clear selected Function
        /// </summary>
        /// <param name="obj"></param>
        private void ClearFunction(object obj)
        {
            SelectedFunction = null;
        }

        /// <summary>
        /// Update-Event if a field was changed
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteUpdateLetterSalut(object obj)
            => LetterSalutation = ContactModel.Language?.CreateLetterSalutation(ContactModel, SelectedFunction);

        /// <summary>
        /// Adds a Title (Title-List)
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// change gender of person and set correct salutation
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeGender(object obj)
        {
            if (Splitted)
            {
                var newSalut = contactModel.Language.Salutations.FirstOrDefault(v => v.Value.Equals(contactModel.Gender));
                if (!string.IsNullOrEmpty(newSalut.Key))
                {
                    ContactSalutation = newSalut.Key;
                }
                else
                {
                    ContactSalutation = string.Empty;
                }
                UpdateLetterSalutCommand?.Execute(this);
            }
        }

        /// <summary>
        /// Save the Contact (Save-Button)
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Checks if a Contact has already been added 
        /// </summary>
        /// <param name="obj"></param>
        private void CheckDuplicate(object obj)
        {
            if (Splitted)
            {
                if (CRMConnector.ContainsContact(contactModel))
                {
                    MessageBox.Show("Kontakt ist bereits im CRM-System vorhanden", "CRM-System");
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
