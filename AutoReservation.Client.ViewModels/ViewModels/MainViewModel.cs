using AutoReservation.Client.ViewModels.Navigation;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System.Threading.Tasks;
using System.Timers;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class MainViewModel : IViewModel
    {
        private INavigationService _navService;

        private IAutoReservationService _target;
        public KundeListViewModel CurrentKundeListViewModel { get; set; }
        public AutoListViewModel CurrentAutoListViewModel { get; set; }
        public ReservationListViewModel CurrentReservationListViewModel { get; set; }

        public RelayCommand OpenDetailKundeWindowCommand { get; set; }
        public RelayCommand OpenDetailAutoWindowCommand { get; set; }
        public RelayCommand OpenDetailReservationWindowCommand { get; set; }
        public RelayCommand OpenSelectedKundeWindowCommand { get; set; }
        public RelayCommand OpenSelectedAutoWindowCommand { get; set; }
        public RelayCommand OpenSelectedReservationWindowCommand { get; set; }

        private Timer _refreshTimer;

        public MainViewModel(INavigationService navService, IAutoReservationService target)
        {
            _navService = navService;
            _target = target;
            CurrentReservationListViewModel = new ReservationListViewModel(target);
            CurrentAutoListViewModel = new AutoListViewModel(target);
            CurrentKundeListViewModel = new KundeListViewModel(target);

            OpenDetailKundeWindowCommand = new RelayCommand(OpenDetailKundeWindow);
            OpenDetailAutoWindowCommand = new RelayCommand(OpenDetailAutoWindow);
            OpenDetailReservationWindowCommand = new RelayCommand(OpenDetailReservationWindow);
            OpenSelectedKundeWindowCommand = new RelayCommand(OpenSelectedKundeWindow);
            OpenSelectedAutoWindowCommand = new RelayCommand(OpenSelectedAutoWindow);
            OpenSelectedReservationWindowCommand = new RelayCommand(OpenSelectedReservationWindow);

            SetTimer();
        }

        private void OpenDetailKundeWindow()
        {
            var vm = new KundeDetailViewModel(_navService, _target, CurrentKundeListViewModel);
            _navService.OpenWindow("KundeDetailWindow", vm);
        }

        private void OpenDetailAutoWindow()
        {
            var vm = new AutoDetailViewModel(_navService, _target, CurrentAutoListViewModel);
            _navService.OpenWindow("AutoDetailWindow", vm);
        }

        private void OpenDetailReservationWindow()
        {
            var vm = new ReservationDetailViewModel(_navService, _target, CurrentReservationListViewModel);
            _navService.OpenWindow("ReservationDetailWindow", vm);
        }

        private void OpenSelectedKundeWindow()
        {
            KundeDto kunde = CurrentKundeListViewModel.SelectedKunde;
            if (kunde != null)
            {
                var vm = new KundeDetailViewModel(_navService, _target, CurrentKundeListViewModel)
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

        private void OpenSelectedAutoWindow()
        {
            AutoDto auto = CurrentAutoListViewModel.SelectedAuto;
            if (auto != null)
            {
                var vm = new AutoDetailViewModel(_navService, _target, CurrentAutoListViewModel)
                {
                    Basistarif = auto.Basistarif,
                    Id = auto.Id,
                    Marke = auto.Marke,
                    RowVersion = auto.RowVersion,
                    Tagestarif = auto.Tagestarif,
                    AutoKlasse = auto.AutoKlasse
                };
                _navService.OpenWindow("AutoDetailWindow", vm);
            }
        }

        private void OpenSelectedReservationWindow()
        {
            ReservationDto reservation = CurrentReservationListViewModel.SelectedReservation;
            if (reservation != null)
            {
                var vm = new ReservationDetailViewModel(_navService, _target, CurrentReservationListViewModel)
                {
                    Bis = reservation.Bis,
                    ReservationNr = reservation.ReservationsNr,
                    RowVersion = reservation.RowVersion,
                    Von = reservation.Von,
                    Auto = reservation.Auto,
                    Kunde = reservation.Kunde
                };
                _navService.OpenWindow("ReservationDetailWindow", vm);
            }
        }

        private void SetTimer()
        {
            _refreshTimer = new Timer(5000);
            _refreshTimer.Elapsed += RefreshLists;
            _refreshTimer.AutoReset = true;
            _refreshTimer.Enabled = true;
        }

        private void RefreshLists(object source, ElapsedEventArgs e)
        {
            CurrentReservationListViewModel.RefreshList();
            CurrentAutoListViewModel.RefreshList();
            CurrentKundeListViewModel.RefreshList();
        }
    }
}
