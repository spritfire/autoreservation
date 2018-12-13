

using System.Windows;

namespace AutoReservation.Client.ViewModels.Navigation
{
    public interface INavigationService
    {
        void OpenWindow(string windowName, object context);
        void CloseWindow(object window);
        void CloseWindow();
    }
}
