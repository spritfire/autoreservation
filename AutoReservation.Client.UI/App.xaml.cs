using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using AutoReservation.Client.ViewModels;

namespace AutoReservation.Client.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            var target = channelFactory.CreateChannel();
            MainWindow = new MainWindow();
            KundeViewModel vmKunde = new KundeViewModel();
            MainWindow.DataContext = vmKunde;
            MainWindow.Show();
        }
    }
}
