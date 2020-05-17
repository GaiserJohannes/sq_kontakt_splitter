using KontaktSplitter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace KontaktSplitter.ViewModels
{
    public class AddTitleViewModel : INotifyPropertyChanged
    {
        public string[] Genders { get; set; }
        public string Title { get; set; }
        public string SelectedGender { get; set; }
      
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddTitleViewModel()
        {
            Genders = Enum.GetNames(typeof(Gender));
            OnPropertyChanged("Genders");
        }





    }
   
}
