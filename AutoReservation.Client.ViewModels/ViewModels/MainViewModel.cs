using AutoReservation.Client.ViewModels.Navigation;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class MainViewModel : IViewModel
    {
        private INavigationService _navService;

        private IAutoReservationService _target;
        public KundeListViewModel Klvm { get; set; }
        public AutoListViewModel Alvm { get; set; }
        public ReservationListViewModel Rlvm { get; set; }

        public RelayCommand openDetailKundeWindowCommand { get; set; }
        public RelayCommand openDetailAutoWindowCommand { get; set; }
        public RelayCommand openDetailReservationWindowCommand { get; set; }
        public RelayCommand refreshListsCommand { get; set; }
        public RelayCommand openSelectedKundeWindowCommand { get; set; }

        public MainViewModel(INavigationService navService, IAutoReservationService target)
        {
            _navService = navService;
            _target = target;
            Rlvm = new ReservationListViewModel(target);
            Alvm = new AutoListViewModel(target);
            Klvm = new KundeListViewModel(target);

            openDetailKundeWindowCommand = new RelayCommand(openDetailKundeWindow);
            openDetailAutoWindowCommand = new RelayCommand(openDetailAutoWindow);
            openDetailReservationWindowCommand = new RelayCommand(openDetailReservationWindow);
            openSelectedKundeWindowCommand = new RelayCommand(openSelectedKundeWindow);
            refreshListsCommand = new RelayCommand(refreshLists);
        }

        private void openDetailKundeWindow()
        {
            var vm = new KundeDetailViewModel(_navService, _target, Klvm);
            _navService.OpenWindow("KundeDetailWindow", vm);
        }

        private void openDetailAutoWindow()
        {
            var vm = new AutoDetailViewModel(_navService, _target);
            _navService.OpenWindow("AutoDetailWindow", vm);
        }

        private void openDetailReservationWindow()
        {
            var vm = new ReservationDetailViewModel(_navService, _target);
            _navService.OpenWindow("ReservationDetailWindow", vm);
        }

        private void openSelectedKundeWindow()
        {
            KundeDto kunde = Klvm.SelectedKunde;
            if (kunde != null)
            {
                var vm = new KundeDetailViewModel(_navService, _target, Klvm)
                {
                    Geburtsdatum = kunde.Geburtsdatum,
                    Id = kunde.Id,
                    Nachname = kunde.Nachname,
                    RowVersion = kunde.RowVersion,
                    Vorname = kunde.Vorname
                };
                _navService.OpenWindow("KundeDetailWindow", vm);
            }
        }

        private void refreshLists()
        {
        }
    }
}
