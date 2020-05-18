using KontaktSplitter.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KontaktSplitter.Views
{
    /// <summary>
    /// Interaction logic for AddTitleWindow.xaml
    /// </summary>
    public partial class AddTitleWindow : Window
    {
       
        public AddTitleWindow(AddTitleViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
