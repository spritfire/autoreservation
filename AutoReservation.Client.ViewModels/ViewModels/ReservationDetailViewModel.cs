using AutoReservation.Client.ViewModels.Navigation;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class ReservationDetailViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigationService _navService;

        public RelayCommand SaveReservationCommand { get; set; }
        public RelayCommand RemoveReservationCommand { get; set; }

        private DateTime _bis;
        private int _reservationsNr;
        private byte[] _rowVersion;
        private DateTime _von;
        private AutoDto _auto;
        private KundeDto _kunde;

        public ReservationListViewModel CurrentReservationListViewModel { get; set; }
        public List<AutoDto> Autos { get; set; }
        public List<KundeDto> Kunden { get; set; }

        private IAutoReservationService _target;

        public ReservationDetailViewModel(INavigationService navService, IAutoReservationService target, ReservationListViewModel currentReservationListViewModel)
        {
            _navService = navService;
            _target = target;
            CurrentReservationListViewModel = currentReservationListViewModel;
            SaveReservationCommand = new RelayCommand(SaveReservation);
            RemoveReservationCommand = new RelayCommand(RemoveReservation);
            Autos = _target.AutoList();
            Kunden = _target.KundeList();
        }

        public DateTime Bis
        {
            get { return _bis; }
            set { SetProperty(ref _bis, value); }
        }

        public int ReservationNr
        {
            get { return _reservationsNr; }
            set { SetProperty(ref _reservationsNr, value); }
        }

        public byte[] RowVersion
        {
            get { return _rowVersion; }
            set { SetProperty(ref _rowVersion, value); }
        }

        public DateTime Von
        {
            get { return _von; }
            set { SetProperty(ref _von, value); }
        }

        public AutoDto Auto
        {
            get { return _auto; }
            set { SetProperty(ref _auto, value); }
        }

        public KundeDto Kunde
        {
            get { return _kunde; }
            set { SetProperty(ref _kunde, value); }
        }

        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return true;
        }

        private void SaveReservation()
        {
            if (_rowVersion == null)
            {
                InsertReservation();
            } else
            {
                UpdateReservation();
            }
        }

        private void InsertReservation()
        {
            try
            {
                _target.InsertReservation(new ReservationDto
                {
                    Bis = _bis,
                    Von = _von,
                    Auto = _auto,
                    Kunde = _kunde
                });
            }
            catch (FaultException<AutoUnavailableFault> e)
            {
                //Handle Fault
            }
            catch (FaultException<InvalidDateRangeFault> e)
            {
                //Handle Fault
            }
            onClose();
        }

        private void UpdateReservation()
        {
            try
            {
                _target.InsertReservation(new ReservationDto
                {
                    Bis = _bis,
                    ReservationsNr = _reservationsNr,
                    RowVersion = _rowVersion,
                    Von = _von,
                    Auto = _auto,
                    Kunde = _kunde
                });
            }
            catch (FaultException<AutoUnavailableFault> e)
            {
                //Handle Fault
            }
            catch (FaultException<InvalidDateRangeFault> e)
            {
                //Handle Fault
            }
            catch (FaultException<OptimisticConcurrencyFault> e)
            {
                //Handle Fault
            }
            onClose();
        }

        private void RemoveReservation()
        {
            try
            {
                _target.InsertReservation(new ReservationDto
                {
                    Bis = _bis,
                    ReservationsNr = _reservationsNr,
                    RowVersion = _rowVersion,
                    Von = _von,
                    Auto = _auto,
                    Kunde = _kunde
                });
            }
            catch (FaultException<OptimisticConcurrencyFault> e)
            {
                //Handle Fault
            }
            onClose();
        }

        private void onClose()
        {
            _navService.CloseWindow();
            CurrentReservationListViewModel.RefreshList();
        }
    }
}
