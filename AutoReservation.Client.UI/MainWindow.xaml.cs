using AutoReservation.Client.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoReservation.Client.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void SelectedKundeDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            vm.OpenSelectedKundeWindowCommand.Execute(null);
        }

        private void SelectedAutoDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            vm.OpenSelectedAutoWindowCommand.Execute(null);
        }

        private void SelectedReservationDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            vm.OpenSelectedReservationWindowCommand.Execute(null);
        }
    }
}
