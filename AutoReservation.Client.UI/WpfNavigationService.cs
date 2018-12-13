
using AutoReservation.Client.UI.DetailWindows;
using System;
using System.Windows;

namespace AutoReservation.Client.ViewModels.Navigation
{
    public class WpfNavigationService : INavigationService
    {
        private Window _lastOpenedWindow;

        public void OpenWindow(string windowName, object context)
        {
            if (windowName == "KundeDetailWindow")
            {
                var win = new KundeDetailWindow();
                win.DataContext = context;
                win.Show();
                _lastOpenedWindow = win;
                return;
            }
            if (windowName == "AutoDetailWindow")
            {
                var win = new AutoDetailWindow();
                win.DataContext = context;
                win.Show();
                _lastOpenedWindow = win;
                return;
            }
            if (windowName == "ReservationDetailWindow")
            {
                var win = new ReservationDetailWindow();
                win.DataContext = context;
                win.Show();
                _lastOpenedWindow = win;
                return;
            }
        }

        public void CloseWindow(object window)
        {
            ((Window)window).Close();
        }

        public void CloseWindow()
        {
            try
            {
                _lastOpenedWindow.Close();
            }
            catch(NullReferenceException) { }
        }
    }
}
